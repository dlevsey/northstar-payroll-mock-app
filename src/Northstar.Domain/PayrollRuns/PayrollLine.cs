namespace Northstar.Domain.PayrollRuns;

public sealed class PayrollLine
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid PayrollRunId { get; set; }
    public Guid TimeEntryId { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public Guid ClassificationId { get; set; }
    public string ClassificationName { get; set; } = string.Empty;
    public DateOnly WorkDate { get; set; }
    public decimal RegularHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public Guid? ResolvedWageRateId { get; set; }
    public decimal? RequiredBaseWages { get; set; }
    public decimal? RequiredFringe { get; set; }
    public decimal ActualBaseWages { get; set; }
    public decimal ActualFringe { get; set; }
    public decimal? Variance { get; set; }
    public int CalculationVersion { get; set; }
}
