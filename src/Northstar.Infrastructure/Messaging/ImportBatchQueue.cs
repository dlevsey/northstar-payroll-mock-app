using System.Threading.Channels;
using Northstar.Application.Abstractions;

namespace Northstar.Infrastructure.Messaging;

public sealed class ImportBatchQueue : IImportBatchQueue
{
    private readonly Channel<Guid> _channel = Channel.CreateUnbounded<Guid>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });

    public ValueTask QueueAsync(Guid importBatchId, CancellationToken cancellationToken) =>
        _channel.Writer.WriteAsync(importBatchId, cancellationToken);

    public IAsyncEnumerable<Guid> ReadAllAsync(CancellationToken cancellationToken) =>
        _channel.Reader.ReadAllAsync(cancellationToken);
}
