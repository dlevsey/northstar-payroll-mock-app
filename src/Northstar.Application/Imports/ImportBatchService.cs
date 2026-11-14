using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Northstar.Application.Abstractions;
using Northstar.Application.Common;
using Northstar.Application.Idempotency;
using Northstar.Domain.Imports;

namespace Northstar.Application.Imports;

public sealed class ImportBatchService(
    IApplicationDbContext dbContext,
    IImportBatchQueue queue,
    IClock clock)
{
    private static readonly HashSet<string> SupportedScenarios = new(StringComparer.OrdinalIgnoreCase)
    {
        "success",
        "mixed",
        "transient",
        "timeout",
        "malformed",
        "permanent"
    };

    public async Task<IReadOnlyList<ImportBatchView>> ListAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        var batches = await dbContext.ImportBatches
            .AsNoTracking()
            .Where(batch => batch.OrganizationId == organizationId)
            .Select(batch => new ImportBatchView(
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
                batch.Version))
            .ToListAsync(cancellationToken);

        // SQLite cannot order DateTimeOffset values. Keep tenant filtering and
        // projection in SQL, then order this intentionally small study history.
        return batches.OrderByDescending(batch => batch.CreatedAt).ToList();
    }

    public async Task<OperationResult<ImportBatchView>> GetAsync(
        Guid organizationId,
        Guid importBatchId,
        CancellationToken cancellationToken)
    {
        var batch = await dbContext.ImportBatches
            .AsNoTracking()
            .SingleOrDefaultAsync(
                value => value.OrganizationId == organizationId && value.Id == importBatchId,
                cancellationToken);

        return batch is null
            ? OperationResult<ImportBatchView>.NotFound()
            : OperationResult<ImportBatchView>.Success(batch.ToView());
    }

    public async Task<OperationResult<IReadOnlyList<ImportRowView>>> GetErrorsAsync(
        Guid organizationId,
        Guid importBatchId,
        CancellationToken cancellationToken)
    {
        var batchExists = await dbContext.ImportBatches
            .AsNoTracking()
            .AnyAsync(
                batch => batch.OrganizationId == organizationId && batch.Id == importBatchId,
                cancellationToken);
        if (!batchExists)
        {
            return OperationResult<IReadOnlyList<ImportRowView>>.NotFound();
        }

        var rows = await dbContext.ImportRows
            .AsNoTracking()
            .Where(row => row.OrganizationId == organizationId &&
                row.ImportBatchId == importBatchId &&
                row.Status != ImportRowStatus.Imported)
            .OrderBy(row => row.RowNumber)
            .Select(row => new ImportRowView(
                row.Id,
                row.ImportBatchId,
                row.RowNumber,
                row.ExternalTimeEntryId,
                row.Status.ToString(),
                row.ErrorCode,
                row.ErrorMessage))
            .ToListAsync(cancellationToken);
        return OperationResult<IReadOnlyList<ImportRowView>>.Success(rows);
    }

    public async Task<OperationResult<ImportBatchView>> CreateAsync(
        Guid organizationId,
        CreateImportBatchRequest request,
        string idempotencyKey,
        CancellationToken cancellationToken)
    {
        var validation = Validate(request, idempotencyKey);
        if (validation is not null)
        {
            return validation;
        }

        var scenario = request.ProviderScenario.Trim().ToLowerInvariant();
        var operation = "CreateImportBatch";
        var requestHash = Hash(
            $"{organizationId:N}:{request.PeriodStart:yyyy-MM-dd}:{request.PeriodEnd:yyyy-MM-dd}:{scenario}");

        ImportBatch batch;
        await using (var transaction = await dbContext.BeginTransactionAsync(cancellationToken))
        {
            var priorResult = await ResolvePriorRequestAsync(
                organizationId,
                operation,
                idempotencyKey,
                requestHash,
                cancellationToken);
            if (priorResult is not null)
            {
                return priorResult;
            }

            var organizationExists = await dbContext.Organizations.AnyAsync(
                organization => organization.Id == organizationId && organization.IsActive,
                cancellationToken);
            if (!organizationExists)
            {
                return OperationResult<ImportBatchView>.NotFound();
            }

            batch = new ImportBatch
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                PeriodStart = request.PeriodStart,
                PeriodEnd = request.PeriodEnd,
                ProviderScenario = scenario,
                CreatedAt = clock.UtcNow
            };

            dbContext.Add(batch);
            dbContext.Add(CreateIdempotencyRecord(
                organizationId,
                operation,
                idempotencyKey,
                requestHash,
                batch.Id));

            try
            {
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync(CancellationToken.None);
                dbContext.ClearTracking();
                var concurrentResult = await ResolvePriorRequestAsync(
                    organizationId,
                    operation,
                    idempotencyKey,
                    requestHash,
                    cancellationToken);
                if (concurrentResult is not null)
                {
                    return concurrentResult;
                }

                throw;
            }
        }

        // SQL is the durable source of pending work. The recurring worker scan closes
        // the process-crash gap if this in-memory notification is ever lost.
        await queue.QueueAsync(batch.Id, CancellationToken.None);
        return OperationResult<ImportBatchView>.Success(batch.ToView());
    }

    public async Task<OperationResult<ImportBatchView>> RetryAsync(
        Guid organizationId,
        Guid importBatchId,
        string idempotencyKey,
        CancellationToken cancellationToken)
    {
        var keyValidation = ValidateIdempotencyKey(idempotencyKey);
        if (keyValidation is not null)
        {
            return keyValidation;
        }

        var operation = $"RetryImportBatch:{importBatchId:N}";
        var requestHash = Hash($"{organizationId:N}:{importBatchId:N}");
        ImportBatch batch;
        await using (var transaction = await dbContext.BeginTransactionAsync(cancellationToken))
        {
            var priorResult = await ResolvePriorRequestAsync(
                organizationId,
                operation,
                idempotencyKey,
                requestHash,
                cancellationToken);
            if (priorResult is not null)
            {
                return priorResult;
            }

            var retryBatch = await dbContext.ImportBatches.SingleOrDefaultAsync(
                value => value.OrganizationId == organizationId && value.Id == importBatchId,
                cancellationToken);

            if (retryBatch is null)
            {
                return OperationResult<ImportBatchView>.NotFound();
            }

            batch = retryBatch;

            if (!batch.Retry())
            {
                return OperationResult<ImportBatchView>.Conflict(
                    "IMPORT_NOT_RETRYABLE",
                    "Only a failed import with a transient provider failure can be retried.");
            }

            dbContext.Add(CreateIdempotencyRecord(
                organizationId,
                operation,
                idempotencyKey,
                requestHash,
                batch.Id));

            try
            {
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync(CancellationToken.None);
                return OperationResult<ImportBatchView>.Conflict(
                    "IMPORT_RETRY_CONFLICT",
                    "The import changed while the retry was being requested. Refresh and try again.");
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync(CancellationToken.None);
                dbContext.ClearTracking();
                var concurrentResult = await ResolvePriorRequestAsync(
                    organizationId,
                    operation,
                    idempotencyKey,
                    requestHash,
                    cancellationToken);
                if (concurrentResult is not null)
                {
                    return concurrentResult;
                }

                throw;
            }
        }

        await queue.QueueAsync(batch.Id, CancellationToken.None);
        return OperationResult<ImportBatchView>.Success(batch.ToView());
    }

    private static OperationResult<ImportBatchView>? Validate(
        CreateImportBatchRequest request,
        string idempotencyKey)
    {
        var keyValidation = ValidateIdempotencyKey(idempotencyKey);
        if (keyValidation is not null)
        {
            return keyValidation;
        }

        if (request.PeriodEnd < request.PeriodStart ||
            request.PeriodEnd.DayNumber - request.PeriodStart.DayNumber > 31)
        {
            return OperationResult<ImportBatchView>.Invalid(
                "INVALID_IMPORT_PERIOD",
                "The import period must be ordered and no longer than 32 days.");
        }

        if (string.IsNullOrWhiteSpace(request.ProviderScenario) ||
            !SupportedScenarios.Contains(request.ProviderScenario.Trim()))
        {
            return OperationResult<ImportBatchView>.Invalid(
                "UNSUPPORTED_PROVIDER_SCENARIO",
                "Use success, mixed, transient, timeout, malformed, or permanent.");
        }

        return null;
    }

    private async Task<OperationResult<ImportBatchView>?> ResolvePriorRequestAsync(
        Guid organizationId,
        string operation,
        string idempotencyKey,
        string requestHash,
        CancellationToken cancellationToken)
    {
        var priorRequest = await dbContext.IdempotencyRecords
            .AsNoTracking()
            .SingleOrDefaultAsync(
                record => record.OrganizationId == organizationId &&
                    record.OperationName == operation &&
                    record.Key == idempotencyKey,
                cancellationToken);
        if (priorRequest is null)
        {
            return null;
        }

        if (!string.Equals(priorRequest.RequestHash, requestHash, StringComparison.Ordinal))
        {
            return OperationResult<ImportBatchView>.Conflict(
                "IDEMPOTENCY_KEY_REUSED",
                "This idempotency key was already used with a different import request.");
        }

        var priorBatch = await dbContext.ImportBatches
            .AsNoTracking()
            .SingleOrDefaultAsync(
                batch => batch.OrganizationId == organizationId &&
                    batch.Id == priorRequest.ResultResourceId,
                cancellationToken);
        return priorBatch is null
            ? OperationResult<ImportBatchView>.NotFound()
            : OperationResult<ImportBatchView>.Success(priorBatch.ToView());
    }

    private IdempotencyRecord CreateIdempotencyRecord(
        Guid organizationId,
        string operation,
        string key,
        string requestHash,
        Guid resourceId) =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            OperationName = operation,
            Key = key,
            RequestHash = requestHash,
            ResultResourceId = resourceId,
            CreatedAt = clock.UtcNow
        };

    private static OperationResult<ImportBatchView>? ValidateIdempotencyKey(string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return OperationResult<ImportBatchView>.Invalid(
                "IDEMPOTENCY_KEY_REQUIRED",
                "The Idempotency-Key header is required.");
        }

        return idempotencyKey.Length > 100
            ? OperationResult<ImportBatchView>.Invalid(
                "IDEMPOTENCY_KEY_TOO_LONG",
                "The Idempotency-Key header must be 100 characters or fewer.")
            : null;
    }

    private static string Hash(string value) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
}
