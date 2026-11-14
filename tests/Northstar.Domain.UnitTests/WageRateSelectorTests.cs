using Northstar.Domain.WageRates;

namespace Northstar.Domain.UnitTests;

public sealed class WageRateSelectorTests
{
    private readonly WageRateSelector _selector = new();
    private readonly Guid _organizationId = Guid.NewGuid();
    private readonly Guid _employeeId = Guid.NewGuid();
    private readonly Guid _projectId = Guid.NewGuid();
    private readonly Guid _classificationId = Guid.NewGuid();

    [Fact]
    public void Select_IncludesBothEffectiveDateBoundaries()
    {
        var rate = Rate(
            WageRateScope.State,
            new DateOnly(2026, 8, 24),
            new DateOnly(2026, 8, 30));

        var firstDay = _selector.Select(Query(new DateOnly(2026, 8, 24)), [rate]);
        var lastDay = _selector.Select(Query(new DateOnly(2026, 8, 30)), [rate]);

        Assert.Equal(RateSelectionStatus.Selected, firstDay.Status);
        Assert.Equal(RateSelectionStatus.Selected, lastDay.Status);
        Assert.Same(rate, firstDay.Rate);
        Assert.Same(rate, lastDay.Rate);
    }

    [Fact]
    public void Select_PrefersProjectRateOverStateRate()
    {
        var state = Rate(WageRateScope.State, new DateOnly(2026, 1, 1), null);
        var project = Rate(WageRateScope.ProjectClassification, new DateOnly(2026, 1, 1), null);

        var result = _selector.Select(Query(new DateOnly(2026, 8, 24)), [state, project]);

        Assert.Equal(RateSelectionStatus.Selected, result.Status);
        Assert.Same(project, result.Rate);
    }

    [Fact]
    public void Select_ReturnsAmbiguousForEquallyRankedOverlappingRates()
    {
        var first = Rate(WageRateScope.State, new DateOnly(2026, 1, 1), null);
        var second = Rate(WageRateScope.State, new DateOnly(2026, 8, 1), null);

        var result = _selector.Select(Query(new DateOnly(2026, 8, 24)), [first, second]);

        Assert.Equal(RateSelectionStatus.Ambiguous, result.Status);
        Assert.Null(result.Rate);
        Assert.Equal(2, result.CandidateIds.Count);
    }

    [Fact]
    public void Select_UsesPriorityToResolveSameScope()
    {
        var ordinary = Rate(WageRateScope.State, new DateOnly(2026, 1, 1), null);
        var priority = Rate(WageRateScope.State, new DateOnly(2026, 1, 1), null);
        priority.Priority = 10;

        var result = _selector.Select(Query(new DateOnly(2026, 8, 24)), [ordinary, priority]);

        Assert.Equal(RateSelectionStatus.Selected, result.Status);
        Assert.Same(priority, result.Rate);
    }

    [Fact]
    public void Select_ReturnsMissingInsteadOfZeroRate()
    {
        var wrongClassification = Rate(WageRateScope.State, new DateOnly(2026, 1, 1), null);
        wrongClassification.ClassificationId = Guid.NewGuid();

        var result = _selector.Select(Query(new DateOnly(2026, 8, 24)), [wrongClassification]);

        Assert.Equal(RateSelectionStatus.Missing, result.Status);
        Assert.Null(result.Rate);
        Assert.Empty(result.CandidateIds);
    }

    private WageRateQuery Query(DateOnly workDate) =>
        new(_organizationId, _employeeId, _projectId, _classificationId, "MN", workDate);

    private WageRate Rate(WageRateScope scope, DateOnly start, DateOnly? end) =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = _organizationId,
            Scope = scope,
            JurisdictionCode = scope == WageRateScope.Federal ? "US" : "MN",
            ProjectId = scope is WageRateScope.ProjectClassification or WageRateScope.EmployeeProjectOverride
                ? _projectId
                : null,
            EmployeeId = scope == WageRateScope.EmployeeProjectOverride ? _employeeId : null,
            ClassificationId = _classificationId,
            EffectiveFrom = start,
            EffectiveTo = end,
            BaseRate = 40m,
            FringeRate = 12m,
            OvertimeMultiplier = 1.5m,
            SourceReference = "TEST"
        };
}
