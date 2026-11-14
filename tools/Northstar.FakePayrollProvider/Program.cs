using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddSingleton<TransientScenarioState>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGet(
        "/api/time-entries",
        async (HttpContext context,
            string organizationReference,
            DateOnly periodStart,
            DateOnly periodEnd,
            string? cursor,
            string? scenario,
            TransientScenarioState transientState) =>
        {
            if (string.IsNullOrWhiteSpace(organizationReference))
            {
                return Results.Problem(
                    title: "Organization reference is required.",
                    detail: "Provide a non-empty organizationReference query parameter.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            if (periodEnd < periodStart)
            {
                return Results.Problem(
                    title: "The requested period is invalid.",
                    detail: "periodEnd must be on or after periodStart.",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            var selectedScenario = (scenario ?? ProviderScenarios.Success).Trim().ToLowerInvariant();
            if (selectedScenario == ProviderScenarios.Timeout)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), context.RequestAborted);
                return Results.Ok(TimeEntryFixtures.Success(
                    organizationReference,
                    periodStart,
                    periodEnd));
            }

            return selectedScenario switch
            {
                ProviderScenarios.Success => CreateSuccessResponse(
                    organizationReference,
                    cursor,
                    periodStart,
                    periodEnd),
                ProviderScenarios.Mixed => Results.Ok(TimeEntryFixtures.Mixed(
                    organizationReference,
                    periodStart,
                    periodEnd)),
                ProviderScenarios.Malformed => Results.Content(
                    "{\"records\":[{\"externalTimeEntryId\":",
                    contentType: "application/json",
                    statusCode: StatusCodes.Status200OK),
                ProviderScenarios.Permanent => Results.Problem(
                    title: "Provider rejected the request.",
                    detail: "This synthetic permanent failure should not be retried.",
                    statusCode: StatusCodes.Status400BadRequest),
                ProviderScenarios.Transient => CreateTransientResponse(
                    context,
                    transientState,
                    $"{organizationReference}:{periodStart:yyyy-MM-dd}:{periodEnd:yyyy-MM-dd}",
                    organizationReference,
                    periodStart,
                    periodEnd),
                _ => Results.Problem(
                    title: "Unknown provider scenario.",
                    detail: $"Scenario '{scenario}' is not supported.",
                    statusCode: StatusCodes.Status400BadRequest)
            };
        })
    .WithName("GetTimeEntries")
    .Produces<TimeEntryPage>()
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status429TooManyRequests);

app.Run();

static IResult CreateSuccessResponse(
    string organizationReference,
    string? cursor,
    DateOnly periodStart,
    DateOnly periodEnd) =>
    cursor switch
    {
        null or "" => Results.Ok(TimeEntryFixtures.SuccessPageOne(organizationReference, periodStart)),
        "page-2" => Results.Ok(TimeEntryFixtures.SuccessPageTwo(
            organizationReference,
            periodStart,
            periodEnd)),
        _ => Results.Problem(
            title: "Provider cursor is invalid.",
            detail: "The supplied synthetic pagination cursor is not recognized.",
            statusCode: StatusCodes.Status400BadRequest)
    };

static IResult CreateTransientResponse(
    HttpContext context,
    TransientScenarioState transientState,
    string requestKey,
    string organizationReference,
    DateOnly periodStart,
    DateOnly periodEnd)
{
    var attempt = transientState.NextAttempt(requestKey);
    if (attempt <= 2)
    {
        context.Response.Headers.RetryAfter = "1";

        return Results.Problem(
            title: "Provider is temporarily throttling requests.",
            detail: "Retry this synthetic transient failure after the delay in Retry-After.",
            statusCode: StatusCodes.Status429TooManyRequests,
            extensions: new Dictionary<string, object?>
            {
                ["attempt"] = attempt
            });
    }

    return Results.Ok(TimeEntryFixtures.Success(organizationReference, periodStart, periodEnd));
}

internal static class ProviderScenarios
{
    public const string Success = "success";
    public const string Mixed = "mixed";
    public const string Malformed = "malformed";
    public const string Permanent = "permanent";
    public const string Transient = "transient";
    public const string Timeout = "timeout";
}

internal sealed class TransientScenarioState
{
    private readonly ConcurrentDictionary<string, int> _attempts = new(StringComparer.Ordinal);

    public int NextAttempt(string requestKey) => _attempts.AddOrUpdate(
        requestKey,
        addValue: 1,
        (_, current) => current >= 3 ? 1 : current + 1);
}

internal static class TimeEntryFixtures
{
    public static TimeEntryPage SuccessPageOne(
        string organizationReference,
        DateOnly periodStart) =>
        new(
            [
                new TimeEntryRecord(
                    ExternalId(organizationReference, periodStart, "001"),
                    "A-EMP-001",
                    "A-PRJ-001",
                    "ELEC",
                    periodStart,
                    8m,
                    0m,
                    "DAY")
            ],
            NextCursor: "page-2");

    public static TimeEntryPage SuccessPageTwo(
        string organizationReference,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        var secondWorkDate = periodStart < periodEnd
            ? periodStart.AddDays(1)
            : periodStart;

        return new TimeEntryPage(
            [
                new TimeEntryRecord(
                    ExternalId(organizationReference, secondWorkDate, "002"),
                    "A-EMP-002",
                    "A-PRJ-001",
                    "LAB",
                    secondWorkDate,
                    7.5m,
                    1m,
                    "DAY")
            ],
            NextCursor: null);
    }

    public static TimeEntryPage Success(
        string organizationReference,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        var secondWorkDate = periodStart < periodEnd
            ? periodStart.AddDays(1)
            : periodStart;

        return new TimeEntryPage(
            [
                new TimeEntryRecord(
                    ExternalId(organizationReference, periodStart, "001"),
                    "A-EMP-001",
                    "A-PRJ-001",
                    "ELEC",
                    periodStart,
                    8m,
                    0m,
                    "DAY"),
                new TimeEntryRecord(
                    ExternalId(organizationReference, secondWorkDate, "002"),
                    "A-EMP-002",
                    "A-PRJ-001",
                    "LAB",
                    secondWorkDate,
                    7.5m,
                    1m,
                    "DAY")
            ],
            NextCursor: null);
    }

    public static TimeEntryPage Mixed(
        string organizationReference,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        var secondWorkDate = periodStart < periodEnd
            ? periodStart.AddDays(1)
            : periodStart;

        return new TimeEntryPage(
            [
                new TimeEntryRecord(
                    ExternalId(organizationReference, periodStart, "MIXED-001"),
                    "A-EMP-001",
                    "A-PRJ-001",
                    "ELEC",
                    periodStart,
                    8m,
                    0m,
                    "DAY"),
                new TimeEntryRecord(
                    ExternalId(organizationReference, periodStart, "MIXED-001"),
                    "A-EMP-002",
                    "A-PRJ-001",
                    "LAB",
                    secondWorkDate,
                    4m,
                    0m,
                    "DAY"),
                new TimeEntryRecord(
                    ExternalId(organizationReference, periodStart, "UNKNOWN-EMPLOYEE"),
                    "A-EMP-999",
                    "A-PRJ-001",
                    "ELEC",
                    periodStart,
                    8m,
                    0m,
                    "DAY"),
                new TimeEntryRecord(
                    ExternalId(organizationReference, secondWorkDate, "NEGATIVE-HOURS"),
                    "A-EMP-001",
                    "A-PRJ-001",
                    "ELEC",
                    secondWorkDate,
                    -1m,
                    0m,
                    "NIGHT")
            ],
            NextCursor: null);
    }

    private static string ExternalId(
        string organizationReference,
        DateOnly workDate,
        string rowIdentity)
    {
        var organizationHash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(organizationReference)))[..8];
        return $"TIME-{organizationHash}-{workDate:yyyyMMdd}-{rowIdentity}";
    }
}

internal sealed record TimeEntryPage(
    IReadOnlyList<TimeEntryRecord> Records,
    string? NextCursor);

internal sealed record TimeEntryRecord(
    string ExternalTimeEntryId,
    string ExternalEmployeeId,
    string ExternalProjectId,
    string ClassificationCode,
    DateOnly WorkDate,
    decimal RegularHours,
    decimal OvertimeHours,
    string ShiftCode);

public partial class Program;
