using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Northstar.Application.Abstractions;
using Northstar.Application.Imports;
using Northstar.Domain.Imports;

namespace Northstar.Infrastructure.Messaging;

public sealed class ImportBatchWorker(
    ImportBatchQueue queue,
    IServiceScopeFactory scopeFactory,
    ILogger<ImportBatchWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scanner = ScanForRecoverableBatchesAsync(stoppingToken);

        try
        {
            await foreach (var importBatchId in queue.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await using var scope = scopeFactory.CreateAsyncScope();
                    var processor = scope.ServiceProvider.GetRequiredService<ImportBatchProcessor>();
                    await processor.ProcessAsync(importBatchId, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception exception)
                {
                    logger.LogError(
                        exception,
                        "Import batch {ImportBatchId} failed with an unexpected worker exception.",
                        importBatchId);
                    await MarkUnexpectedFailureAsync(importBatchId);
                }
            }
        }
        finally
        {
            try
            {
                await scanner;
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // Expected host shutdown.
            }
        }
    }

    private async Task ScanForRecoverableBatchesAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        do
        {
            try
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var now = DateTimeOffset.UtcNow;
                var candidates = await dbContext.ImportBatches
                    .AsNoTracking()
                    .Where(batch => batch.Status == ImportBatchStatus.Pending ||
                        batch.Status == ImportBatchStatus.Processing)
                    .Select(batch => new
                    {
                        batch.Id,
                        batch.Status,
                        batch.ProcessingLeaseExpiresAt
                    })
                    .ToListAsync(cancellationToken);
                var pendingIds = candidates
                    .Where(batch => batch.Status == ImportBatchStatus.Pending ||
                        batch.ProcessingLeaseExpiresAt is null ||
                        batch.ProcessingLeaseExpiresAt <= now)
                    .Select(batch => batch.Id);

                foreach (var pendingId in pendingIds)
                {
                    await queue.QueueAsync(pendingId, cancellationToken);
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "The import recovery scan failed and will be retried.");
            }
        }
        while (await timer.WaitForNextTickAsync(cancellationToken));
    }

    private async Task MarkUnexpectedFailureAsync(Guid importBatchId)
    {
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        try
        {
            await using var scope = scopeFactory.CreateAsyncScope();
            var processor = scope.ServiceProvider.GetRequiredService<ImportBatchProcessor>();
            await processor.MarkUnexpectedFailureAsync(importBatchId, timeout.Token);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Import batch {ImportBatchId} could not be marked failed after an unexpected error.",
                importBatchId);
        }
    }
}
