using System.Net;
using System.Net.Http.Json;
using Northstar.Application.PayrollRuns;
using Northstar.Application.WageRates;
using Northstar.Domain.WageRates;
using Northstar.Infrastructure.Persistence;

namespace Northstar.Api.IntegrationTests;

public sealed class PayrollRunsApiTests
{
    [Fact]
    public async Task Health_ReturnsHealthy()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync(new Uri("/health", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Healthy", await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task Calculate_ProducesExpectedLinesAndFindings()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        var response = await Calculate(client, "calculate-first");
        var run = await response.Content.ReadFromJsonAsync<PayrollRunView>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(run);
        Assert.Equal("NeedsReview", run.Status);
        Assert.Equal(4, run.Lines.Count);
        Assert.Contains(run.Findings, finding => finding.Code == "RATE_NOT_FOUND" && finding.Severity == "Blocker");
        Assert.Contains(run.Findings, finding => finding.Code == "FRINGE_SHORTFALL" && finding.Severity == "Warning");
    }

    [Fact]
    public async Task Calculate_WithSameIdempotencyKey_ReturnsOriginalResultWithoutDuplicateLines()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        var firstResponse = await Calculate(client, "calculate-retry");
        var first = await firstResponse.Content.ReadFromJsonAsync<PayrollRunView>();
        var secondResponse = await Calculate(client, "calculate-retry");
        var second = await secondResponse.Content.ReadFromJsonAsync<PayrollRunView>();

        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, secondResponse.StatusCode);
        Assert.NotNull(first);
        Assert.NotNull(second);
        Assert.Equal(first.CalculationVersion, second.CalculationVersion);
        Assert.Equal(first.Lines.Select(line => line.Id), second.Lines.Select(line => line.Id));
    }

    [Fact]
    public async Task Get_RunOwnedByAnotherTenant_ReturnsNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationB);

        var response = await client.GetAsync(new Uri($"/api/payroll-runs/{DemoDataIds.RunA}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Calculate_WithoutIdempotencyKey_ReturnsUnprocessableEntity()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        var response = await client.PostAsJsonAsync(
            new Uri($"/api/payroll-runs/{DemoDataIds.RunA}/calculate", UriKind.Relative),
            new { });

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        Assert.Contains("IDEMPOTENCY_KEY_REQUIRED", await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task AddMissingRate_RecalculateAndApprove_CompletesFirstSlice()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);
        var rateRequest = new CreateWageRateRequest(
            WageRateScope.ProjectClassification,
            "MN",
            DemoDataIds.ProjectA,
            null,
            DemoDataIds.CarpenterA,
            new DateOnly(2026, 8, 24),
            null,
            30m,
            7m,
            1.5m,
            0,
            "INTERVIEW-CORRECTION");

        var createRate = await client.PostAsJsonAsync(
            new Uri("/api/wage-rates", UriKind.Relative),
            rateRequest);
        var calculate = await Calculate(client, "calculate-after-rate-fix");
        var readyRun = await calculate.Content.ReadFromJsonAsync<PayrollRunView>();
        Assert.NotNull(readyRun);

        var approve = await client.PostAsJsonAsync(
            new Uri($"/api/payroll-runs/{DemoDataIds.RunA}/approve", UriKind.Relative),
            new { expectedVersion = readyRun.Version });
        var approvedRun = await approve.Content.ReadFromJsonAsync<PayrollRunView>();

        Assert.Equal(HttpStatusCode.Created, createRate.StatusCode);
        Assert.Equal(HttpStatusCode.OK, calculate.StatusCode);
        Assert.Equal("ReadyForApproval", readyRun.Status);
        Assert.Equal(HttpStatusCode.OK, approve.StatusCode);
        Assert.NotNull(approvedRun);
        Assert.Equal("Approved", approvedRun.Status);
    }

    private static HttpClient TenantClient(CustomWebApplicationFactory factory, Guid organizationId)
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Organization-Id", organizationId.ToString());
        client.DefaultRequestHeaders.Add("X-User-Role", "PayrollManager");
        return client;
    }

    private static async Task<HttpResponseMessage> Calculate(HttpClient client, string idempotencyKey)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            new Uri($"/api/payroll-runs/{DemoDataIds.RunA}/calculate", UriKind.Relative))
        {
            Content = JsonContent.Create(new { })
        };
        request.Headers.Add("Idempotency-Key", idempotencyKey);
        return await client.SendAsync(request);
    }
}
