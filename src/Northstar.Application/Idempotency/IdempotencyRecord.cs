namespace Northstar.Application.Idempotency;

public sealed class IdempotencyRecord
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string OperationName { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string RequestHash { get; set; } = string.Empty;
    public Guid ResultResourceId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
