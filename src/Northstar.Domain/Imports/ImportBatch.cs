namespace Northstar.Domain.Imports;

public enum ImportBatchStatus
{
    Pending,
    Processing,
    Completed,
    CompletedWithErrors,
    Failed
}

public sealed class ImportBatch
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public string ProviderScenario { get; set; } = "success";
    public ImportBatchStatus Status { get; private set; } = ImportBatchStatus.Pending;
    public int TotalRows { get; private set; }
    public int ImportedRows { get; private set; }
    public int ErrorRows { get; private set; }
    public int DuplicateRows { get; private set; }
    public int AttemptCount { get; private set; }
    public string? FailureCode { get; private set; }
    public string? FailureMessage { get; private set; }
    public bool CanRetry { get; private set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? ProcessingLeaseExpiresAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public long Version { get; private set; } = 1;
    public List<ImportRow> Rows { get; } = [];

    public bool BeginProcessing(DateTimeOffset startedAt, TimeSpan leaseDuration)
    {
        var canClaim = Status == ImportBatchStatus.Pending ||
            (Status == ImportBatchStatus.Processing &&
                (ProcessingLeaseExpiresAt is null || ProcessingLeaseExpiresAt <= startedAt));
        if (!canClaim)
        {
            return false;
        }

        if (leaseDuration <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(leaseDuration));
        }

        Status = ImportBatchStatus.Processing;
        StartedAt = startedAt;
        ProcessingLeaseExpiresAt = startedAt.Add(leaseDuration);
        CompletedAt = null;
        FailureCode = null;
        FailureMessage = null;
        CanRetry = false;
        AttemptCount++;
        Version++;
        return true;
    }

    public void Complete(
        int totalRows,
        int importedRows,
        int errorRows,
        int duplicateRows,
        DateTimeOffset completedAt)
    {
        if (Status != ImportBatchStatus.Processing)
        {
            throw new InvalidOperationException("Only a processing import batch can be completed.");
        }

        if (totalRows < 0 || importedRows < 0 || errorRows < 0 || duplicateRows < 0 ||
            importedRows + errorRows + duplicateRows != totalRows)
        {
            throw new ArgumentOutOfRangeException(nameof(totalRows), "Import row counts must be nonnegative and add up to the total.");
        }

        TotalRows = totalRows;
        ImportedRows = importedRows;
        ErrorRows = errorRows;
        DuplicateRows = duplicateRows;
        Status = errorRows > 0 || duplicateRows > 0
            ? ImportBatchStatus.CompletedWithErrors
            : ImportBatchStatus.Completed;
        CompletedAt = completedAt;
        ProcessingLeaseExpiresAt = null;
        CanRetry = false;
        Version++;
    }

    public void Fail(string code, string message, bool canRetry, DateTimeOffset failedAt)
    {
        if (Status != ImportBatchStatus.Processing)
        {
            throw new InvalidOperationException("Only a processing import batch can fail.");
        }

        Status = ImportBatchStatus.Failed;
        FailureCode = code;
        FailureMessage = message;
        CanRetry = canRetry;
        CompletedAt = failedAt;
        ProcessingLeaseExpiresAt = null;
        Version++;
    }

    public bool Retry()
    {
        if (Status != ImportBatchStatus.Failed || !CanRetry)
        {
            return false;
        }

        Status = ImportBatchStatus.Pending;
        FailureCode = null;
        FailureMessage = null;
        CanRetry = false;
        StartedAt = null;
        ProcessingLeaseExpiresAt = null;
        CompletedAt = null;
        Version++;
        return true;
    }
}
