namespace Northstar.Infrastructure.Integrations;

public sealed class PayrollProviderOptions
{
    public const string SectionName = "PayrollProvider";

    public Uri BaseUrl { get; set; } = new("http://localhost:5130");
    public int MaxAttempts { get; set; } = 3;
    public int BaseRetryDelayMilliseconds { get; set; } = 100;
    public int AttemptTimeoutMilliseconds { get; set; } = 10_000;
}
