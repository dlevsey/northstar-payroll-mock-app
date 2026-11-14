namespace Northstar.Domain.Imports;

public enum ImportRowStatus
{
    Imported,
    Invalid,
    Duplicate
}

public sealed class ImportRow
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid ImportBatchId { get; set; }
    public int RowNumber { get; set; }
    public string? ExternalTimeEntryId { get; set; }
    public string? ExternalEmployeeId { get; set; }
    public string? ExternalProjectId { get; set; }
    public string? ClassificationCode { get; set; }
    public DateOnly? WorkDate { get; set; }
    public decimal? RegularHours { get; set; }
    public decimal? OvertimeHours { get; set; }
    public string? ShiftCode { get; set; }
    public ImportRowStatus Status { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string SourcePayload { get; set; } = string.Empty;
}
