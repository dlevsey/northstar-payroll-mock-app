namespace Northstar.Domain.WageRates;

public enum RateSelectionStatus
{
    Selected,
    Missing,
    Ambiguous
}

public sealed record RateSelectionResult(
    RateSelectionStatus Status,
    WageRate? Rate,
    IReadOnlyList<Guid> CandidateIds)
{
    public static RateSelectionResult Selected(WageRate rate) =>
        new(RateSelectionStatus.Selected, rate, [rate.Id]);

    public static RateSelectionResult Missing() =>
        new(RateSelectionStatus.Missing, null, []);

    public static RateSelectionResult Ambiguous(IEnumerable<WageRate> rates) =>
        new(RateSelectionStatus.Ambiguous, null, rates.Select(rate => rate.Id).ToArray());
}
