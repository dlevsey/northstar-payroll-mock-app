using Northstar.Domain.PayrollRuns;
using Northstar.Domain.WageRates;

namespace Northstar.Domain.UnitTests;

public sealed class PayrollCalculatorTests
{
    [Fact]
    public void Calculate_UsesTheRateEffectiveOnEachWorkDate()
    {
        var ids = new TestIds();
        var firstRate = ids.Rate(new DateOnly(2026, 8, 24), new DateOnly(2026, 8, 25), 40m);
        var secondRate = ids.Rate(new DateOnly(2026, 8, 26), null, 42m);
        var calculator = new PayrollCalculator(new WageRateSelector());

        var result = calculator.Calculate(
            ids.RunId,
            1,
            [ids.Segment(new DateOnly(2026, 8, 25)), ids.Segment(new DateOnly(2026, 8, 26))],
            [firstRate, secondRate]);

        Assert.Equal(320m, result.Lines[0].RequiredBaseWages);
        Assert.Equal(336m, result.Lines[1].RequiredBaseWages);
        Assert.Empty(result.Findings);
    }

    [Fact]
    public void Calculate_CreatesBlockerAndLeavesRequiredAmountsNullWhenRateIsMissing()
    {
        var ids = new TestIds();
        var calculator = new PayrollCalculator(new WageRateSelector());

        var result = calculator.Calculate(ids.RunId, 1, [ids.Segment(new DateOnly(2026, 8, 24))], []);

        var line = Assert.Single(result.Lines);
        Assert.Null(line.ResolvedWageRateId);
        Assert.Null(line.RequiredBaseWages);
        Assert.Null(line.RequiredFringe);
        var finding = Assert.Single(result.Findings);
        Assert.Equal(FindingSeverity.Blocker, finding.Severity);
        Assert.Equal("RATE_NOT_FOUND", finding.Code);
    }

    [Fact]
    public void Calculate_AppliesOvertimeAndRoundsMoneyAwayFromZero()
    {
        var ids = new TestIds();
        var rate = ids.Rate(new DateOnly(2026, 1, 1), null, 31.333m);
        var calculator = new PayrollCalculator(new WageRateSelector());
        var segment = ids.Segment(new DateOnly(2026, 8, 24)) with
        {
            RegularHours = 7.25m,
            OvertimeHours = 1.5m
        };

        var result = calculator.Calculate(ids.RunId, 1, [segment], [rate]);

        var line = Assert.Single(result.Lines);
        Assert.Equal(297.66m, line.RequiredBaseWages);
        Assert.Equal(105m, line.RequiredFringe);
    }

    private sealed class TestIds
    {
        public Guid OrganizationId { get; } = Guid.NewGuid();
        public Guid EmployeeId { get; } = Guid.NewGuid();
        public Guid ProjectId { get; } = Guid.NewGuid();
        public Guid ClassificationId { get; } = Guid.NewGuid();
        public Guid RunId { get; } = Guid.NewGuid();

        public PayrollWorkSegment Segment(DateOnly date) =>
            new(
                Guid.NewGuid(),
                OrganizationId,
                EmployeeId,
                "Test Employee",
                45m,
                12m,
                ProjectId,
                "Test Project",
                "MN",
                ClassificationId,
                "Electrician",
                date,
                8m,
                0m);

        public WageRate Rate(DateOnly start, DateOnly? end, decimal baseRate) =>
            new()
            {
                Id = Guid.NewGuid(),
                OrganizationId = OrganizationId,
                Scope = WageRateScope.ProjectClassification,
                JurisdictionCode = "MN",
                ProjectId = ProjectId,
                ClassificationId = ClassificationId,
                EffectiveFrom = start,
                EffectiveTo = end,
                BaseRate = baseRate,
                FringeRate = 12m,
                OvertimeMultiplier = 1.5m,
                SourceReference = "TEST"
            };
    }
}
