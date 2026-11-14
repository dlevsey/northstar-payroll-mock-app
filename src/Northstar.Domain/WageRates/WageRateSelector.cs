namespace Northstar.Domain.WageRates;

public sealed record WageRateQuery(
    Guid OrganizationId,
    Guid EmployeeId,
    Guid ProjectId,
    Guid ClassificationId,
    string JurisdictionCode,
    DateOnly WorkDate);

public sealed class WageRateSelector
{
    public RateSelectionResult Select(WageRateQuery query, IEnumerable<WageRate> candidates)
    {
        ArgumentNullException.ThrowIfNull(candidates);

        var matching = candidates
            .Where(rate => Matches(query, rate))
            .ToArray();

        if (matching.Length == 0)
        {
            return RateSelectionResult.Missing();
        }

        var bestScope = matching.Min(rate => rate.Scope);
        var scoped = matching.Where(rate => rate.Scope == bestScope).ToArray();
        var bestPriority = scoped.Max(rate => rate.Priority);
        var finalists = scoped
            .Where(rate => rate.Priority == bestPriority)
            .OrderBy(rate => rate.EffectiveFrom)
            .ThenBy(rate => rate.Id)
            .ToArray();

        return finalists.Length == 1
            ? RateSelectionResult.Selected(finalists[0])
            : RateSelectionResult.Ambiguous(finalists);
    }

    private static bool Matches(WageRateQuery query, WageRate rate)
    {
        if (rate.OrganizationId != query.OrganizationId ||
            rate.ClassificationId != query.ClassificationId ||
            !rate.IsEffectiveOn(query.WorkDate))
        {
            return false;
        }

        return rate.Scope switch
        {
            WageRateScope.EmployeeProjectOverride =>
                rate.EmployeeId == query.EmployeeId && rate.ProjectId == query.ProjectId,
            WageRateScope.ProjectClassification =>
                rate.EmployeeId is null && rate.ProjectId == query.ProjectId,
            WageRateScope.Union =>
                rate.EmployeeId is null && rate.ProjectId is null &&
                IsJurisdictionMatch(rate.JurisdictionCode, query.JurisdictionCode),
            WageRateScope.State =>
                rate.EmployeeId is null && rate.ProjectId is null &&
                string.Equals(rate.JurisdictionCode, query.JurisdictionCode, StringComparison.OrdinalIgnoreCase),
            WageRateScope.Federal =>
                rate.EmployeeId is null && rate.ProjectId is null &&
                string.Equals(rate.JurisdictionCode, "US", StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    private static bool IsJurisdictionMatch(string candidate, string requested) =>
        candidate == "*" || string.Equals(candidate, requested, StringComparison.OrdinalIgnoreCase);
}
