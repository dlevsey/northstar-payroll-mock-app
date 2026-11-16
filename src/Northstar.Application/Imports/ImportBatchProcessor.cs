using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Northstar.Application.Abstractions;
using Northstar.Domain.Imports;
using Northstar.Domain.TimeEntries;

namespace Northstar.Application.Imports;

public sealed class ImportBatchProcessor(
    IApplicationDbContext dbContext,
    IPayrollProviderClient providerClient,
    IClock clock)
{
    private static readonly TimeSpan ProcessingLease = TimeSpan.FromMinutes(2);

    public async Task ProcessAsync(
        Guid importBatchId,
        CancellationToken cancellationToken)
    {
        var batch = await dbContext.ImportBatches.SingleOrDefaultAsync(
            value => value.Id == importBatchId,
            cancellationToken);
        if (batch is null || !batch.BeginProcessing(clock.UtcNow, ProcessingLease))
        {
            return;
        }

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            // Another worker won the optimistic claim for this batch.
            return;
        }

        var organization = await dbContext.Organizations
            .AsNoTracking()
            .SingleOrDefaultAsync(value => value.Id == batch.OrganizationId, cancellationToken);
        if (organization is null)
        {
            batch.Fail(
                "ORGANIZATION_NOT_FOUND",
                "The import organization no longer exists.",
                canRetry: false,
                clock.UtcNow);
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        IReadOnlyList<ProviderTimeEntryRecord> records;
        try
        {
            records = await providerClient.GetTimeEntriesAsync(
                new ProviderImportRequest(
                    organization.ExternalReference,
                    batch.PeriodStart,
                    batch.PeriodEnd,
                    batch.ProviderScenario),
                cancellationToken);
        }
        catch (PayrollProviderTransientException exception)
        {
            batch.Fail(
                "PROVIDER_TRANSIENT_FAILURE",
                exception.Message,
                canRetry: true,
                clock.UtcNow);
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
        catch (PayrollProviderContractException exception)
        {
            batch.Fail(
                "PROVIDER_CONTRACT_FAILURE",
                exception.Message,
                canRetry: false,
                clock.UtcNow);
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        var employees = (await dbContext.Employees
                .AsNoTracking()
                .Where(employee => employee.OrganizationId == batch.OrganizationId)
                .ToListAsync(cancellationToken))
            .ToDictionary(employee => employee.ExternalEmployeeId, StringComparer.OrdinalIgnoreCase);
        var projects = (await dbContext.Projects
                .AsNoTracking()
                .Where(project => project.OrganizationId == batch.OrganizationId)
                .ToListAsync(cancellationToken))
            .ToDictionary(project => project.ExternalProjectId, StringComparer.OrdinalIgnoreCase);
        var classifications = (await dbContext.WorkClassifications
                .AsNoTracking()
                .Where(classification => classification.OrganizationId == batch.OrganizationId)
                .ToListAsync(cancellationToken))
            .ToDictionary(classification => classification.Code, StringComparer.OrdinalIgnoreCase);

        var incomingExternalIds = records
            .Select(record => record.ExternalTimeEntryId)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Cast<string>()
            .Select(NormalizeExternalTimeEntryId)
            .Distinct(StringComparer.Ordinal)
            .ToArray();
        var existingExternalIds = await dbContext.TimeEntries
            .AsNoTracking()
            .Where(entry => entry.OrganizationId == batch.OrganizationId &&
                incomingExternalIds.Contains(entry.ExternalTimeEntryId))
            .Select(entry => entry.ExternalTimeEntryId)
            .ToListAsync(cancellationToken);
        var seenExternalIds = existingExternalIds
            .Select(NormalizeExternalTimeEntryId)
            .ToHashSet(StringComparer.Ordinal);

        var importedRows = 0;
        var errorRows = 0;
        var duplicateRows = 0;
        await using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        for (var index = 0; index < records.Count; index++)
        {
            var record = records[index];
            var validation = Validate(
                batch,
                record,
                seenExternalIds,
                employees,
                projects,
                classifications);
            var row = CreateRow(batch, record, index + 1);

            if (validation.Status == ImportRowStatus.Duplicate)
            {
                duplicateRows++;
                ApplyError(row, validation);
            }
            else if (validation.Status == ImportRowStatus.Invalid)
            {
                errorRows++;
                ApplyError(row, validation);
            }
            else
            {
                row.Status = ImportRowStatus.Imported;
                importedRows++;
                dbContext.Add(new TimeEntry
                {
                    Id = Guid.NewGuid(),
                    OrganizationId = batch.OrganizationId,
                    ExternalTimeEntryId = NormalizeExternalTimeEntryId(record.ExternalTimeEntryId!),
                    EmployeeId = employees[record.ExternalEmployeeId!].Id,
                    ProjectId = projects[record.ExternalProjectId!].Id,
                    ClassificationId = classifications[record.ClassificationCode!].Id,
                    WorkDate = record.WorkDate!.Value,
                    RegularHours = record.RegularHours!.Value,
                    OvertimeHours = record.OvertimeHours!.Value,
                    ShiftCode = record.ShiftCode,
                    ImportedInBatchId = batch.Id
                });
            }

            batch.Rows.Add(row);
            dbContext.Add(row);
        }

        batch.Complete(records.Count, importedRows, errorRows, duplicateRows, clock.UtcNow);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task MarkUnexpectedFailureAsync(
        Guid importBatchId,
        CancellationToken cancellationToken)
    {
        var batch = await dbContext.ImportBatches.SingleOrDefaultAsync(
            value => value.Id == importBatchId,
            cancellationToken);
        if (batch is null || batch.Status != ImportBatchStatus.Processing)
        {
            return;
        }

        batch.Fail(
            "IMPORT_PROCESSING_FAILURE",
            "The import stopped unexpectedly. Review the trace and retry the batch.",
            canRetry: true,
            clock.UtcNow);
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            // The batch was recovered or completed by another worker.
        }
    }

    private static ImportRow CreateRow(
        ImportBatch batch,
        ProviderTimeEntryRecord record,
        int rowNumber) =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = batch.OrganizationId,
            ImportBatchId = batch.Id,
            RowNumber = rowNumber,
            ExternalTimeEntryId = Truncate(record.ExternalTimeEntryId, 100),
            ExternalEmployeeId = Truncate(record.ExternalEmployeeId, 100),
            ExternalProjectId = Truncate(record.ExternalProjectId, 100),
            ClassificationCode = Truncate(record.ClassificationCode, 50),
            WorkDate = record.WorkDate,
            RegularHours = record.RegularHours,
            OvertimeHours = record.OvertimeHours,
            ShiftCode = Truncate(record.ShiftCode, 20),
            SourcePayload = JsonSerializer.Serialize(new
            {
                ExternalTimeEntryId = Truncate(record.ExternalTimeEntryId, 100),
                ExternalEmployeeId = Truncate(record.ExternalEmployeeId, 100),
                ExternalProjectId = Truncate(record.ExternalProjectId, 100),
                ClassificationCode = Truncate(record.ClassificationCode, 50),
                record.WorkDate,
                record.RegularHours,
                record.OvertimeHours,
                ShiftCode = Truncate(record.ShiftCode, 20)
            })
        };

    private static RowValidation Validate(
        ImportBatch batch,
        ProviderTimeEntryRecord record,
        HashSet<string> seenExternalIds,
        IReadOnlyDictionary<string, Domain.Employees.Employee> employees,
        IReadOnlyDictionary<string, Domain.Projects.Project> projects,
        IReadOnlyDictionary<string, Domain.Projects.WorkClassification> classifications)
    {
        if (string.IsNullOrWhiteSpace(record.ExternalTimeEntryId) ||
            string.IsNullOrWhiteSpace(record.ExternalEmployeeId) ||
            string.IsNullOrWhiteSpace(record.ExternalProjectId) ||
            string.IsNullOrWhiteSpace(record.ClassificationCode) ||
            record.WorkDate is null ||
            record.RegularHours is null ||
            record.OvertimeHours is null)
        {
            return RowValidation.Invalid("REQUIRED_FIELD_MISSING", "One or more required provider fields are missing.");
        }

        if (record.ExternalTimeEntryId.Length > 100 ||
            record.ExternalEmployeeId.Length > 100 ||
            record.ExternalProjectId.Length > 100 ||
            record.ClassificationCode.Length > 50 ||
            record.ShiftCode?.Length > 20)
        {
            return RowValidation.Invalid(
                "FIELD_TOO_LONG",
                "One or more provider identifiers exceed the supported length.");
        }

        if (!seenExternalIds.Add(NormalizeExternalTimeEntryId(record.ExternalTimeEntryId)))
        {
            return RowValidation.Duplicate(
                "DUPLICATE_TIME_ENTRY",
                "The external time-entry identifier was already imported or repeated in this payload.");
        }

        if (record.WorkDate < batch.PeriodStart || record.WorkDate > batch.PeriodEnd)
        {
            return RowValidation.Invalid("WORK_DATE_OUTSIDE_PERIOD", "The work date is outside the import period.");
        }

        if (record.RegularHours < 0 || record.OvertimeHours < 0 ||
            record.RegularHours + record.OvertimeHours <= 0)
        {
            return RowValidation.Invalid("INVALID_HOURS", "Hours must be nonnegative and total more than zero.");
        }

        if (!employees.TryGetValue(record.ExternalEmployeeId, out var employee))
        {
            return RowValidation.Invalid("EMPLOYEE_NOT_FOUND", "The provider employee is not mapped to this organization.");
        }

        var workDate = record.WorkDate.Value;
        if (workDate < employee.ActiveFrom ||
            (employee.ActiveTo is { } employeeActiveTo && workDate > employeeActiveTo))
        {
            return RowValidation.Invalid(
                "EMPLOYEE_NOT_ACTIVE_ON_WORK_DATE",
                "The employee is not active on the imported work date.");
        }

        if (!projects.TryGetValue(record.ExternalProjectId, out var project))
        {
            return RowValidation.Invalid("PROJECT_NOT_FOUND", "The provider project is not mapped to this organization.");
        }

        if (!project.IsActive)
        {
            return RowValidation.Invalid(
                "PROJECT_INACTIVE",
                "The imported project is inactive.");
        }

        if (workDate < project.StartsOn ||
            (project.EndsOn is { } projectEndsOn && workDate > projectEndsOn))
        {
            return RowValidation.Invalid(
                "PROJECT_NOT_ACTIVE_ON_WORK_DATE",
                "The project is outside its active date range on the imported work date.");
        }

        if (!classifications.ContainsKey(record.ClassificationCode))
        {
            return RowValidation.Invalid("CLASSIFICATION_NOT_FOUND", "The provider classification is not mapped.");
        }

        return RowValidation.Imported();
    }

    private static string? Truncate(string? value, int maximumLength) =>
        value is not null && value.Length > maximumLength
            ? value[..maximumLength]
            : value;

    private static string NormalizeExternalTimeEntryId(string value) =>
        value.Trim().ToUpperInvariant();

    private static void ApplyError(ImportRow row, RowValidation validation)
    {
        row.Status = validation.Status;
        row.ErrorCode = validation.ErrorCode;
        row.ErrorMessage = validation.ErrorMessage;
    }

    private sealed record RowValidation(
        ImportRowStatus Status,
        string? ErrorCode = null,
        string? ErrorMessage = null)
    {
        public static RowValidation Imported() => new(ImportRowStatus.Imported);
        public static RowValidation Invalid(string code, string message) =>
            new(ImportRowStatus.Invalid, code, message);
        public static RowValidation Duplicate(string code, string message) =>
            new(ImportRowStatus.Duplicate, code, message);
    }
}
