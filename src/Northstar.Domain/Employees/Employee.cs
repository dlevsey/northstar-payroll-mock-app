namespace Northstar.Domain.Employees;

public sealed class Employee
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string ExternalEmployeeId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string HomeJurisdiction { get; set; } = string.Empty;
    public decimal BaseHourlyRate { get; set; }
    public decimal ActualFringeRate { get; set; }
    public DateOnly ActiveFrom { get; set; }
    public DateOnly? ActiveTo { get; set; }
    public long Version { get; set; } = 1;
}
