namespace Northstar.Application.PayrollRuns;

public sealed record PayrollLineView(
    Guid Id,
    Guid EmployeeId,
    string EmployeeName,
    string ProjectName,
    string ClassificationName,
    DateOnly WorkDate,
    decimal RegularHours,
    decimal OvertimeHours,
    Guid? ResolvedWageRateId,
    decimal? RequiredBaseWages,
    decimal? RequiredFringe,
    decimal ActualBaseWages,
    decimal ActualFringe,
    decimal? Variance);

public sealed record ComplianceFindingView(
    Guid Id,
    string Severity,
    string Code,
    string Message,
    Guid? TimeEntryId);

public sealed record PayrollRunView(
    Guid Id,
    Guid OrganizationId,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    string Status,
    int CalculationVersion,
    long Version,
    DateTimeOffset? ApprovedAt,
    IReadOnlyList<PayrollLineView> Lines,
    IReadOnlyList<ComplianceFindingView> Findings);
