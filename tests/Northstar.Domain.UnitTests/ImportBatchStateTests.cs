using Northstar.Domain.Imports;

namespace Northstar.Domain.UnitTests;

public sealed class ImportBatchStateTests
{
    private static readonly DateTimeOffset Now = new(2026, 8, 30, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void BeginProcessing_ClaimsPendingBatchAndCreatesLease()
    {
        var batch = Batch();
        var leaseDuration = TimeSpan.FromMinutes(5);

        var claimed = batch.BeginProcessing(Now, leaseDuration);

        Assert.True(claimed);
        Assert.Equal(ImportBatchStatus.Processing, batch.Status);
        Assert.Equal(Now, batch.StartedAt);
        Assert.Equal(Now.Add(leaseDuration), batch.ProcessingLeaseExpiresAt);
        Assert.Equal(1, batch.AttemptCount);
        Assert.Equal(2, batch.Version);
    }

    [Fact]
    public void BeginProcessing_RejectsClaimWhileExistingLeaseIsActive()
    {
        var batch = Batch();
        Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));
        var originalLeaseExpiration = batch.ProcessingLeaseExpiresAt;

        var claimed = batch.BeginProcessing(Now.AddMinutes(4), TimeSpan.FromMinutes(10));

        Assert.False(claimed);
        Assert.Equal(Now, batch.StartedAt);
        Assert.Equal(originalLeaseExpiration, batch.ProcessingLeaseExpiresAt);
        Assert.Equal(1, batch.AttemptCount);
        Assert.Equal(2, batch.Version);
    }

    [Fact]
    public void BeginProcessing_ReclaimsBatchWhenLeaseHasExpired()
    {
        var batch = Batch();
        Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));
        var reclaimedAt = Now.AddMinutes(5);
        var newLeaseDuration = TimeSpan.FromMinutes(10);

        var claimed = batch.BeginProcessing(reclaimedAt, newLeaseDuration);

        Assert.True(claimed);
        Assert.Equal(ImportBatchStatus.Processing, batch.Status);
        Assert.Equal(reclaimedAt, batch.StartedAt);
        Assert.Equal(reclaimedAt.Add(newLeaseDuration), batch.ProcessingLeaseExpiresAt);
        Assert.Equal(2, batch.AttemptCount);
        Assert.Equal(3, batch.Version);
    }

    [Fact]
    public void Complete_UsesCompletedWithErrorsWhenRowsAreDuplicates()
    {
        var batch = Batch();
        Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));
        var completedAt = Now.AddMinutes(1);

        batch.Complete(
            totalRows: 3,
            importedRows: 2,
            errorRows: 0,
            duplicateRows: 1,
            completedAt);

        Assert.Equal(ImportBatchStatus.CompletedWithErrors, batch.Status);
        Assert.Equal(3, batch.TotalRows);
        Assert.Equal(2, batch.ImportedRows);
        Assert.Equal(0, batch.ErrorRows);
        Assert.Equal(1, batch.DuplicateRows);
        Assert.Equal(completedAt, batch.CompletedAt);
        Assert.Null(batch.ProcessingLeaseExpiresAt);
        Assert.False(batch.CanRetry);
    }

    [Theory]
    [InlineData(-1, 0, 0, 0)]
    [InlineData(1, -1, 1, 1)]
    [InlineData(1, 1, -1, 1)]
    [InlineData(1, 1, 1, -1)]
    [InlineData(3, 1, 0, 1)]
    public void Complete_RejectsNegativeOrInconsistentCounts(
        int totalRows,
        int importedRows,
        int errorRows,
        int duplicateRows)
    {
        var batch = Batch();
        Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));

        Assert.Throws<ArgumentOutOfRangeException>(() => batch.Complete(
            totalRows,
            importedRows,
            errorRows,
            duplicateRows,
            Now.AddMinutes(1)));

        Assert.Equal(ImportBatchStatus.Processing, batch.Status);
        Assert.Equal(0, batch.TotalRows);
        Assert.Equal(0, batch.ImportedRows);
        Assert.Equal(0, batch.ErrorRows);
        Assert.Equal(0, batch.DuplicateRows);
    }

    [Fact]
    public void Retry_AllowsOnlyFailuresMarkedAsRetryable()
    {
        var transientFailure = ProcessingBatch();
        transientFailure.Fail(
            "PROVIDER_TRANSIENT",
            "Provider temporarily unavailable.",
            canRetry: true,
            Now.AddMinutes(1));

        var permanentFailure = ProcessingBatch();
        permanentFailure.Fail(
            "PROVIDER_CONTRACT",
            "Provider response was invalid.",
            canRetry: false,
            Now.AddMinutes(1));

        Assert.True(transientFailure.Retry());
        Assert.Equal(ImportBatchStatus.Pending, transientFailure.Status);
        Assert.Null(transientFailure.FailureCode);
        Assert.Null(transientFailure.FailureMessage);
        Assert.Null(transientFailure.StartedAt);
        Assert.Null(transientFailure.CompletedAt);
        Assert.False(transientFailure.CanRetry);

        Assert.False(permanentFailure.Retry());
        Assert.Equal(ImportBatchStatus.Failed, permanentFailure.Status);
        Assert.Equal("PROVIDER_CONTRACT", permanentFailure.FailureCode);
        Assert.False(permanentFailure.CanRetry);
    }

    private static ImportBatch ProcessingBatch()
    {
        var batch = Batch();
        Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));
        return batch;
    }

    private static ImportBatch Batch() =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            PeriodStart = new DateOnly(2026, 8, 24),
            PeriodEnd = new DateOnly(2026, 8, 30),
            ProviderScenario = "success",
            CreatedAt = Now
        };
}
