namespace Northstar.Domain.TimeEntries;

public sealed class TimeEntry
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string ExternalTimeEntryId { get; set; } = string.Empty;
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ClassificationId { get; set; }
    public DateOnly WorkDate { get; set; }
    public decimal RegularHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public string? ShiftCode { get; set; }
    public Guid? ImportedInBatchId { get; set; }
}
