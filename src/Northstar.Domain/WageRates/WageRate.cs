namespace Northstar.Domain.WageRates;

public enum WageRateScope
{
    EmployeeProjectOverride = 0,
    ProjectClassification = 1,
    Union = 2,
    State = 3,
    Federal = 4
}

public sealed class WageRate
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public WageRateScope Scope { get; set; }
    public string JurisdictionCode { get; set; } = string.Empty;
    public Guid? ProjectId { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid ClassificationId { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public decimal BaseRate { get; set; }
    public decimal FringeRate { get; set; }
    public decimal OvertimeMultiplier { get; set; } = 1.5m;
    public int Priority { get; set; }
    public string SourceReference { get; set; } = string.Empty;
    public long Version { get; set; } = 1;

    public bool IsEffectiveOn(DateOnly workDate) =>
        workDate >= EffectiveFrom && (EffectiveTo is null || workDate <= EffectiveTo.Value);
}
