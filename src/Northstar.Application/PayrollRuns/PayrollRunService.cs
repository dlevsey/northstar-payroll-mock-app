using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Northstar.Application.Abstractions;
using Northstar.Application.Common;
using Northstar.Application.Idempotency;
using Northstar.Domain.PayrollRuns;
using Northstar.Domain.WageRates;

namespace Northstar.Application.PayrollRuns;

public sealed class PayrollRunService(
    IApplicationDbContext dbContext,
    PayrollCalculator calculator,
    IClock clock)
{
    public async Task<IReadOnlyList<PayrollRunView>> ListAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        var runs = await dbContext.PayrollRuns
            .AsNoTracking()
            .Where(run => run.OrganizationId == organizationId)
            .Include(run => run.Lines)
            .Include(run => run.Findings)
            .OrderByDescending(run => run.PeriodStart)
            .ToListAsync(cancellationToken);

        return runs.Select(Map).ToArray();
    }

    public async Task<OperationResult<PayrollRunView>> GetAsync(
        Guid organizationId,
        Guid runId,
        CancellationToken cancellationToken)
    {
        var run = await LoadAsync(organizationId, runId, track: false, cancellationToken);
        return run is null
            ? OperationResult<PayrollRunView>.NotFound()
            : OperationResult<PayrollRunView>.Success(Map(run));
    }

    public async Task<OperationResult<PayrollRunView>> CalculateAsync(
        Guid organizationId,
        Guid runId,
        string idempotencyKey,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return OperationResult<PayrollRunView>.Invalid(
                "IDEMPOTENCY_KEY_REQUIRED",
                "The Idempotency-Key header is required.");
        }

        if (idempotencyKey.Length > 100)
        {
            return OperationResult<PayrollRunView>.Invalid(
                "IDEMPOTENCY_KEY_TOO_LONG",
                "The Idempotency-Key header must be 100 characters or fewer.");
        }

        var operation = "CalculatePayrollRun";
        var requestHash = Hash($"{organizationId:N}:{runId:N}");

        await using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        var priorRequest = await dbContext.IdempotencyRecords
            .SingleOrDefaultAsync(
                record => record.OrganizationId == organizationId &&
                    record.OperationName == operation &&
                    record.Key == idempotencyKey,
                cancellationToken);

        if (priorRequest is not null)
        {
            if (!string.Equals(priorRequest.RequestHash, requestHash, StringComparison.Ordinal))
            {
                return OperationResult<PayrollRunView>.Conflict(
                    "IDEMPOTENCY_KEY_REUSED",
                    "This idempotency key was already used with a different request.");
            }

            var priorRun = await LoadAsync(organizationId, priorRequest.ResultResourceId, track: false, cancellationToken);
            return priorRun is null
                ? OperationResult<PayrollRunView>.NotFound()
                : OperationResult<PayrollRunView>.Success(Map(priorRun));
        }

        var run = await LoadAsync(organizationId, runId, track: true, cancellationToken);
        if (run is null)
        {
            return OperationResult<PayrollRunView>.NotFound();
        }

        var beginResult = run.BeginCalculation();
        if (beginResult.Status != PayrollRunActionStatus.Success)
        {
            return OperationResult<PayrollRunView>.Conflict(
                beginResult.ErrorCode ?? "INVALID_STATE",
                "The payroll run cannot be calculated in its current state.");
        }

        var segments = await (
            from entry in dbContext.TimeEntries.AsNoTracking()
            join employee in dbContext.Employees.AsNoTracking() on entry.EmployeeId equals employee.Id
            join project in dbContext.Projects.AsNoTracking() on entry.ProjectId equals project.Id
            join classification in dbContext.WorkClassifications.AsNoTracking() on entry.ClassificationId equals classification.Id
            where entry.OrganizationId == organizationId &&
                employee.OrganizationId == organizationId &&
                project.OrganizationId == organizationId &&
                classification.OrganizationId == organizationId &&
                entry.WorkDate >= run.PeriodStart &&
                entry.WorkDate <= run.PeriodEnd
            orderby entry.WorkDate, employee.DisplayName, classification.Name
            select new PayrollWorkSegment(
                entry.Id,
                entry.OrganizationId,
                employee.Id,
                employee.DisplayName,
                employee.BaseHourlyRate,
                employee.ActualFringeRate,
                project.Id,
                project.Name,
                project.JurisdictionCode,
                classification.Id,
                classification.Name,
                entry.WorkDate,
                entry.RegularHours,
                entry.OvertimeHours))
            .ToListAsync(cancellationToken);

        var classificationIds = segments.Select(segment => segment.ClassificationId).Distinct().ToArray();
        var rates = await dbContext.WageRates
            .AsNoTracking()
            .Where(rate => rate.OrganizationId == organizationId &&
                classificationIds.Contains(rate.ClassificationId) &&
                rate.EffectiveFrom <= run.PeriodEnd &&
                (rate.EffectiveTo == null || rate.EffectiveTo >= run.PeriodStart))
            .ToListAsync(cancellationToken);

        var calculation = calculator.Calculate(
            run.Id,
            run.CalculationVersion + 1,
            segments,
            rates);
        run.CompleteCalculation(calculation);

        foreach (var line in run.Lines)
        {
            dbContext.Add(line);
        }

        foreach (var finding in run.Findings)
        {
            dbContext.Add(finding);
        }

        dbContext.Add(new IdempotencyRecord
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            OperationName = operation,
            Key = idempotencyKey,
            RequestHash = requestHash,
            ResultResourceId = run.Id,
            CreatedAt = clock.UtcNow
        });

        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return OperationResult<PayrollRunView>.Success(Map(run));
    }

    public async Task<OperationResult<PayrollRunView>> ApproveAsync(
        Guid organizationId,
        Guid runId,
        long expectedVersion,
        CancellationToken cancellationToken)
    {
        var run = await LoadAsync(organizationId, runId, track: true, cancellationToken);
        if (run is null)
        {
            return OperationResult<PayrollRunView>.NotFound();
        }

        var approval = run.Approve(expectedVersion, clock.UtcNow);
        if (approval.Status == PayrollRunActionStatus.Blocked)
        {
            return OperationResult<PayrollRunView>.Invalid(
                approval.ErrorCode ?? "APPROVAL_BLOCKED",
                "The payroll run has blocker findings.");
        }

        if (approval.Status == PayrollRunActionStatus.Conflict)
        {
            return OperationResult<PayrollRunView>.Conflict(
                approval.ErrorCode ?? "INVALID_STATE",
                "The payroll run changed or is not ready for approval.");
        }

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return OperationResult<PayrollRunView>.Conflict(
                "VERSION_CONFLICT",
                "The payroll run was updated by another request.");
        }

        return OperationResult<PayrollRunView>.Success(Map(run));
    }

    private async Task<PayrollRun?> LoadAsync(
        Guid organizationId,
        Guid runId,
        bool track,
        CancellationToken cancellationToken)
    {
        IQueryable<PayrollRun> query = dbContext.PayrollRuns
            .Where(run => run.OrganizationId == organizationId && run.Id == runId)
            .Include(run => run.Lines)
            .Include(run => run.Findings);

        if (!track)
        {
            query = query.AsNoTracking();
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    private static PayrollRunView Map(PayrollRun run) =>
        new(
            run.Id,
            run.OrganizationId,
            run.PeriodStart,
            run.PeriodEnd,
            run.Status.ToString(),
            run.CalculationVersion,
            run.Version,
            run.ApprovedAt,
            run.Lines
                .OrderBy(line => line.WorkDate)
                .ThenBy(line => line.EmployeeName)
                .Select(line => new PayrollLineView(
                    line.Id,
                    line.EmployeeId,
                    line.EmployeeName,
                    line.ProjectName,
                    line.ClassificationName,
                    line.WorkDate,
                    line.RegularHours,
                    line.OvertimeHours,
                    line.ResolvedWageRateId,
                    line.RequiredBaseWages,
                    line.RequiredFringe,
                    line.ActualBaseWages,
                    line.ActualFringe,
                    line.Variance))
                .ToArray(),
            run.Findings
                .OrderBy(finding => finding.Severity)
                .ThenBy(finding => finding.Code)
                .Select(finding => new ComplianceFindingView(
                    finding.Id,
                    finding.Severity.ToString(),
                    finding.Code,
                    finding.Message,
                    finding.TimeEntryId))
                .ToArray());

    private static string Hash(string value) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
}
