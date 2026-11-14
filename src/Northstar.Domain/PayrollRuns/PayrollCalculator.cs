using Northstar.Domain.WageRates;

namespace Northstar.Domain.PayrollRuns;

public sealed record PayrollWorkSegment(
    Guid TimeEntryId,
    Guid OrganizationId,
    Guid EmployeeId,
    string EmployeeName,
    decimal EmployeeBaseRate,
    decimal EmployeeFringeRate,
    Guid ProjectId,
    string ProjectName,
    string JurisdictionCode,
    Guid ClassificationId,
    string ClassificationName,
    DateOnly WorkDate,
    decimal RegularHours,
    decimal OvertimeHours);

public sealed record PayrollCalculation(
    IReadOnlyList<PayrollLine> Lines,
    IReadOnlyList<ComplianceFinding> Findings);

public sealed class PayrollCalculator(WageRateSelector rateSelector)
{
    public PayrollCalculation Calculate(
        Guid payrollRunId,
        int calculationVersion,
        IEnumerable<PayrollWorkSegment> workSegments,
        IReadOnlyCollection<WageRate> candidateRates)
    {
        ArgumentNullException.ThrowIfNull(workSegments);
        ArgumentNullException.ThrowIfNull(candidateRates);

        var lines = new List<PayrollLine>();
        var findings = new List<ComplianceFinding>();

        foreach (var segment in workSegments)
        {
            ValidateHours(segment);

            var selected = rateSelector.Select(
                new WageRateQuery(
                    segment.OrganizationId,
                    segment.EmployeeId,
                    segment.ProjectId,
                    segment.ClassificationId,
                    segment.JurisdictionCode,
                    segment.WorkDate),
                candidateRates);

            var actualBase = Money(
                (segment.RegularHours * segment.EmployeeBaseRate) +
                (segment.OvertimeHours * segment.EmployeeBaseRate * 1.5m));
            var actualFringe = Money(
                (segment.RegularHours + segment.OvertimeHours) * segment.EmployeeFringeRate);

            var line = new PayrollLine
            {
                Id = Guid.NewGuid(),
                OrganizationId = segment.OrganizationId,
                PayrollRunId = payrollRunId,
                TimeEntryId = segment.TimeEntryId,
                EmployeeId = segment.EmployeeId,
                EmployeeName = segment.EmployeeName,
                ProjectId = segment.ProjectId,
                ProjectName = segment.ProjectName,
                ClassificationId = segment.ClassificationId,
                ClassificationName = segment.ClassificationName,
                WorkDate = segment.WorkDate,
                RegularHours = segment.RegularHours,
                OvertimeHours = segment.OvertimeHours,
                ActualBaseWages = actualBase,
                ActualFringe = actualFringe,
                CalculationVersion = calculationVersion
            };

            if (selected.Status != RateSelectionStatus.Selected)
            {
                findings.Add(CreateRateFinding(payrollRunId, segment, selected));
                lines.Add(line);
                continue;
            }

            var rate = selected.Rate!;
            var requiredBase = Money(
                (segment.RegularHours * rate.BaseRate) +
                (segment.OvertimeHours * rate.BaseRate * rate.OvertimeMultiplier));
            var requiredFringe = Money(
                (segment.RegularHours + segment.OvertimeHours) * rate.FringeRate);

            line.ResolvedWageRateId = rate.Id;
            line.RequiredBaseWages = requiredBase;
            line.RequiredFringe = requiredFringe;
            line.Variance = Money(actualBase + actualFringe - requiredBase - requiredFringe);
            lines.Add(line);

            if (actualBase < requiredBase)
            {
                findings.Add(new ComplianceFinding
                {
                    Id = Guid.NewGuid(),
                    OrganizationId = segment.OrganizationId,
                    PayrollRunId = payrollRunId,
                    TimeEntryId = segment.TimeEntryId,
                    Severity = FindingSeverity.Blocker,
                    Code = "BASE_WAGE_SHORTFALL",
                    Message = $"{segment.EmployeeName} is short {requiredBase - actualBase:C} in required cash wages."
                });
            }

            if (actualFringe < requiredFringe)
            {
                findings.Add(new ComplianceFinding
                {
                    Id = Guid.NewGuid(),
                    OrganizationId = segment.OrganizationId,
                    PayrollRunId = payrollRunId,
                    TimeEntryId = segment.TimeEntryId,
                    Severity = FindingSeverity.Warning,
                    Code = "FRINGE_SHORTFALL",
                    Message = $"{segment.EmployeeName} is short {requiredFringe - actualFringe:C} in fringe contributions."
                });
            }
        }

        return new PayrollCalculation(lines, findings);
    }

    private static ComplianceFinding CreateRateFinding(
        Guid payrollRunId,
        PayrollWorkSegment segment,
        RateSelectionResult selected) =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = segment.OrganizationId,
            PayrollRunId = payrollRunId,
            TimeEntryId = segment.TimeEntryId,
            Severity = FindingSeverity.Blocker,
            Code = selected.Status == RateSelectionStatus.Missing ? "RATE_NOT_FOUND" : "RATE_AMBIGUOUS",
            Message = selected.Status == RateSelectionStatus.Missing
                ? $"No effective rate was found for {segment.ClassificationName} on {segment.WorkDate:yyyy-MM-dd}."
                : $"Multiple equally ranked rates apply to {segment.ClassificationName} on {segment.WorkDate:yyyy-MM-dd}."
        };

    private static void ValidateHours(PayrollWorkSegment segment)
    {
        if (segment.RegularHours < 0 || segment.OvertimeHours < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(segment), "Payroll hours cannot be negative.");
        }
    }

    private static decimal Money(decimal amount) => decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
}
