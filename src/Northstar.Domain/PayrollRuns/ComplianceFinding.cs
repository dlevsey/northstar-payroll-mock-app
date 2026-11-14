namespace Northstar.Domain.PayrollRuns;

public enum FindingSeverity
{
    Blocker,
    Warning,
    Information
}

public sealed class ComplianceFinding
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid PayrollRunId { get; set; }
    public Guid? TimeEntryId { get; set; }
    public FindingSeverity Severity { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
