namespace Northstar.Application.Imports;

public sealed record CreateImportBatchRequest(
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    string ProviderScenario = "success");

public sealed record ImportBatchView(
    Guid Id,
    Guid OrganizationId,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    string ProviderScenario,
    string Status,
    int TotalRows,
    int ImportedRows,
    int ErrorRows,
    int DuplicateRows,
    int AttemptCount,
    string? FailureCode,
    string? FailureMessage,
    bool CanRetry,
    DateTimeOffset CreatedAt,
    DateTimeOffset? CompletedAt,
    long Version);

public sealed record ImportRowView(
    Guid Id,
    Guid ImportBatchId,
    int RowNumber,
    string? ExternalTimeEntryId,
    string Status,
    string? ErrorCode,
    string? ErrorMessage);
