namespace Northstar.Application.Abstractions;

public interface IImportBatchQueue
{
    ValueTask QueueAsync(
        Guid importBatchId,
        CancellationToken cancellationToken);
}
