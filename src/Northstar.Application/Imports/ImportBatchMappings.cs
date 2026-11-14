using Northstar.Domain.Imports;

namespace Northstar.Application.Imports;

internal static class ImportBatchMappings
{
    public static ImportBatchView ToView(this ImportBatch batch) =>
        new(
            batch.Id,
            batch.OrganizationId,
            batch.PeriodStart,
            batch.PeriodEnd,
            batch.ProviderScenario,
            batch.Status.ToString(),
            batch.TotalRows,
            batch.ImportedRows,
            batch.ErrorRows,
            batch.DuplicateRows,
            batch.AttemptCount,
            batch.FailureCode,
            batch.FailureMessage,
            batch.CanRetry,
            batch.CreatedAt,
            batch.CompletedAt,
            batch.Version);

    public static ImportRowView ToView(this ImportRow row) =>
        new(
            row.Id,
            row.ImportBatchId,
            row.RowNumber,
            row.ExternalTimeEntryId,
            row.Status.ToString(),
            row.ErrorCode,
            row.ErrorMessage);
}
