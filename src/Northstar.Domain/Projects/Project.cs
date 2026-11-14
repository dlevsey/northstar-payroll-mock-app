namespace Northstar.Domain.Projects;

public sealed class Project
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string ExternalProjectId { get; set; } = string.Empty;
    public string ContractNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string JurisdictionCode { get; set; } = string.Empty;
    public string GoverningAuthority { get; set; } = string.Empty;
    public string WageDeterminationNumber { get; set; } = string.Empty;
    public DateOnly StartsOn { get; set; }
    public DateOnly? EndsOn { get; set; }
    public bool IsActive { get; set; } = true;
    public long Version { get; set; } = 1;
}
