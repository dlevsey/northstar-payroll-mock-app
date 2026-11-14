using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northstar.Application.Imports;
using Northstar.Infrastructure.Persistence;

namespace Northstar.Api.IntegrationTests;

public sealed class ImportBatchesApiTests
{
    private static readonly DateOnly PeriodStart = new(2026, 8, 24);
    private static readonly DateOnly PeriodEnd = new(2026, 8, 30);

    [Fact]
    public async Task Create_ReturnsAccepted_ThenWorkerCompletesTheBatch()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var response = await CreateImportAsync(client, "success", "success-accepted");
        var accepted = await ReadBatchAsync(response);

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.Contains(accepted.Id.ToString(), response.Headers.Location.ToString(), StringComparison.OrdinalIgnoreCase);

        var completed = await WaitForTerminalBatchAsync(client, accepted.Id);

        Assert.Equal("Completed", completed.Status);
        Assert.Equal(2, completed.TotalRows);
        Assert.Equal(2, completed.ImportedRows);
        Assert.Equal(0, completed.ErrorRows);
        Assert.Equal(0, completed.DuplicateRows);
        Assert.Equal(1, completed.AttemptCount);
        Assert.Equal(1, factory.PayrollProvider.GetCallCount("success"));

        using var listResponse = await client.GetAsync(new Uri("/api/import-batches", UriKind.Relative));
        var batches = await listResponse.Content.ReadFromJsonAsync<List<ImportBatchView>>();
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        Assert.NotNull(batches);
        Assert.Equal(accepted.Id, Assert.Single(batches).Id);
    }

    [Fact]
    public async Task Create_WithSameIdempotencyKey_ReturnsTheOriginalBatch()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var firstResponse = await CreateImportAsync(client, "success", "create-replay");
        var first = await ReadBatchAsync(firstResponse);
        var completed = await WaitForTerminalBatchAsync(client, first.Id);

        using var replayResponse = await CreateImportAsync(client, "success", "create-replay");
        var replay = await ReadBatchAsync(replayResponse);

        Assert.Equal(HttpStatusCode.Accepted, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Accepted, replayResponse.StatusCode);
        Assert.Equal(first.Id, replay.Id);
        Assert.Equal(completed.Status, replay.Status);
        Assert.Equal(1, factory.PayrollProvider.GetCallCount("success"));
        Assert.Equal(1, await CountImportBatchesAsync(factory));
    }

    [Fact]
    public async Task Create_WithSameKeyAndDifferentBody_ReturnsConflict()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var firstResponse = await CreateImportAsync(client, "success", "create-body-conflict");
        var first = await ReadBatchAsync(firstResponse);
        _ = await WaitForTerminalBatchAsync(client, first.Id);

        using var conflictResponse = await CreateImportAsync(client, "mixed", "create-body-conflict");
        var problem = await conflictResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Conflict, conflictResponse.StatusCode);
        Assert.Contains("IDEMPOTENCY_KEY_REUSED", problem, StringComparison.Ordinal);
        Assert.Equal(0, factory.PayrollProvider.GetCallCount("mixed"));
    }

    [Fact]
    public async Task MixedImport_PersistsValidRowAndReturnsRowLevelErrors()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var response = await CreateImportAsync(client, "mixed", "mixed-outcomes");
        var accepted = await ReadBatchAsync(response);
        var completed = await WaitForTerminalBatchAsync(client, accepted.Id);

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Assert.Equal("CompletedWithErrors", completed.Status);
        Assert.Equal(4, completed.TotalRows);
        Assert.Equal(1, completed.ImportedRows);
        Assert.Equal(2, completed.ErrorRows);
        Assert.Equal(1, completed.DuplicateRows);

        using var errorsResponse = await client.GetAsync(
            new Uri($"/api/import-batches/{accepted.Id}/errors", UriKind.Relative));
        var errors = await errorsResponse.Content.ReadFromJsonAsync<List<ImportRowView>>();

        Assert.Equal(HttpStatusCode.OK, errorsResponse.StatusCode);
        Assert.NotNull(errors);
        Assert.Collection(
            errors,
            row =>
            {
                Assert.Equal(2, row.RowNumber);
                Assert.Equal("Duplicate", row.Status);
                Assert.Equal("DUPLICATE_TIME_ENTRY", row.ErrorCode);
            },
            row =>
            {
                Assert.Equal(3, row.RowNumber);
                Assert.Equal("Invalid", row.Status);
                Assert.Equal("EMPLOYEE_NOT_FOUND", row.ErrorCode);
            },
            row =>
            {
                Assert.Equal(4, row.RowNumber);
                Assert.Equal("Invalid", row.Status);
                Assert.Equal("INVALID_HOURS", row.ErrorCode);
            });

        Assert.Equal(1, await CountImportedEntriesAsync(factory, "TEST-MIXED-"));
    }

    [Fact]
    public async Task ImportBeforeEmployeeActiveFrom_ReturnsExplicitRowError()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);
        await SetEmployeeActiveFromAsync(factory, DemoDataIds.EmployeeAvery, PeriodStart.AddDays(1));

        using var response = await CreateImportAsync(client, "success", "employee-pre-active");
        var accepted = await ReadBatchAsync(response);
        var completed = await WaitForTerminalBatchAsync(client, accepted.Id);
        var errors = await GetErrorsAsync(client, accepted.Id);

        Assert.Equal("CompletedWithErrors", completed.Status);
        Assert.Equal(1, completed.ImportedRows);
        Assert.Equal(1, completed.ErrorRows);
        var error = Assert.Single(errors);
        Assert.Equal(1, error.RowNumber);
        Assert.Equal("EMPLOYEE_NOT_ACTIVE_ON_WORK_DATE", error.ErrorCode);
    }

    [Fact]
    public async Task ImportBeforeProjectStartsOn_ReturnsExplicitRowError()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);
        await SetProjectStartsOnAsync(factory, DemoDataIds.ProjectA, PeriodStart.AddDays(1));

        using var response = await CreateImportAsync(client, "success", "project-pre-active");
        var accepted = await ReadBatchAsync(response);
        var completed = await WaitForTerminalBatchAsync(client, accepted.Id);
        var errors = await GetErrorsAsync(client, accepted.Id);

        Assert.Equal("CompletedWithErrors", completed.Status);
        Assert.Equal(1, completed.ImportedRows);
        Assert.Equal(1, completed.ErrorRows);
        var error = Assert.Single(errors);
        Assert.Equal(1, error.RowNumber);
        Assert.Equal("PROJECT_NOT_ACTIVE_ON_WORK_DATE", error.ErrorCode);
    }

    [Fact]
    public async Task ImportForInactiveProject_ReturnsExplicitRowErrors()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);
        await SetProjectActiveAsync(factory, DemoDataIds.ProjectA, isActive: false);

        using var response = await CreateImportAsync(client, "success", "project-inactive");
        var accepted = await ReadBatchAsync(response);
        var completed = await WaitForTerminalBatchAsync(client, accepted.Id);
        var errors = await GetErrorsAsync(client, accepted.Id);

        Assert.Equal("CompletedWithErrors", completed.Status);
        Assert.Equal(0, completed.ImportedRows);
        Assert.Equal(2, completed.ErrorRows);
        Assert.Equal(2, errors.Count);
        Assert.All(errors, error => Assert.Equal("PROJECT_INACTIVE", error.ErrorCode));
    }

    [Fact]
    public async Task DuplicateProviderDelivery_DoesNotCreateDuplicateTimeEntries()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var firstResponse = await CreateImportAsync(client, "success", "delivery-one");
        var first = await ReadBatchAsync(firstResponse);
        var firstCompleted = await WaitForTerminalBatchAsync(client, first.Id);

        using var secondResponse = await CreateImportAsync(client, "success", "delivery-two");
        var second = await ReadBatchAsync(secondResponse);
        var secondCompleted = await WaitForTerminalBatchAsync(client, second.Id);

        Assert.Equal("Completed", firstCompleted.Status);
        Assert.Equal("CompletedWithErrors", secondCompleted.Status);
        Assert.Equal(0, secondCompleted.ImportedRows);
        Assert.Equal(0, secondCompleted.ErrorRows);
        Assert.Equal(2, secondCompleted.DuplicateRows);
        Assert.Equal(2, await CountImportedEntriesAsync(factory, "TEST-IMPORT-"));
        Assert.Equal(2, factory.PayrollProvider.GetCallCount("success"));

        using var errorsResponse = await client.GetAsync(
            new Uri($"/api/import-batches/{second.Id}/errors", UriKind.Relative));
        var duplicateRows = await errorsResponse.Content.ReadFromJsonAsync<List<ImportRowView>>();
        Assert.Equal(HttpStatusCode.OK, errorsResponse.StatusCode);
        Assert.NotNull(duplicateRows);
        Assert.Equal(2, duplicateRows.Count);
        Assert.All(duplicateRows, row => Assert.Equal("Duplicate", row.Status));
    }

    [Fact]
    public async Task CaseVariantProviderReplay_DoesNotCreateASecondTimeEntry()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var firstResponse = await CreateImportAsync(client, "success", "case-delivery-one");
        var first = await ReadBatchAsync(firstResponse);
        var firstCompleted = await WaitForTerminalBatchAsync(client, first.Id);

        factory.PayrollProvider.ReturnCaseVariantOnNextSuccess();
        using var secondResponse = await CreateImportAsync(client, "success", "case-delivery-two");
        var second = await ReadBatchAsync(secondResponse);
        var secondCompleted = await WaitForTerminalBatchAsync(client, second.Id);

        Assert.Equal("Completed", firstCompleted.Status);
        Assert.Equal("CompletedWithErrors", secondCompleted.Status);
        Assert.Equal(0, secondCompleted.ImportedRows);
        Assert.Equal(2, secondCompleted.DuplicateRows);
        Assert.Equal(2, await CountImportedEntriesAsync(factory, "TEST-IMPORT-"));

        var externalIds = await GetImportedEntryIdsAsync(factory, "TEST-IMPORT-");
        Assert.Equal(["TEST-IMPORT-001", "TEST-IMPORT-002"], externalIds);
    }

    [Fact]
    public async Task GetBatchAndErrors_FromAnotherTenant_ReturnNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var ownerClient = TenantClient(factory, DemoDataIds.OrganizationA);
        using var otherTenantClient = TenantClient(factory, DemoDataIds.OrganizationB);

        using var createResponse = await CreateImportAsync(ownerClient, "mixed", "tenant-boundary");
        var batch = await ReadBatchAsync(createResponse);
        _ = await WaitForTerminalBatchAsync(ownerClient, batch.Id);

        using var batchResponse = await otherTenantClient.GetAsync(
            new Uri($"/api/import-batches/{batch.Id}", UriKind.Relative));
        using var errorsResponse = await otherTenantClient.GetAsync(
            new Uri($"/api/import-batches/{batch.Id}/errors", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, batchResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, errorsResponse.StatusCode);
    }

    [Theory]
    [InlineData("permanent")]
    [InlineData("malformed")]
    public async Task ProviderContractFailure_MarksBatchAsPermanentlyFailed(string scenario)
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var response = await CreateImportAsync(client, scenario, $"{scenario}-failure");
        var accepted = await ReadBatchAsync(response);
        var failed = await WaitForTerminalBatchAsync(client, accepted.Id);

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Assert.Equal("Failed", failed.Status);
        Assert.Equal("PROVIDER_CONTRACT_FAILURE", failed.FailureCode);
        Assert.False(failed.CanRetry);
        Assert.Equal(0, failed.ImportedRows);
        Assert.Equal(1, failed.AttemptCount);
        Assert.Equal(1, factory.PayrollProvider.GetCallCount(scenario));
    }

    [Fact]
    public async Task TransientFailure_RetryWithSameKey_CompletesExactlyOnce()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = TenantClient(factory, DemoDataIds.OrganizationA);

        using var createResponse = await CreateImportAsync(client, "transient", "transient-create");
        var accepted = await ReadBatchAsync(createResponse);
        var firstFailure = await WaitForTerminalBatchAsync(client, accepted.Id);

        Assert.Equal("Failed", firstFailure.Status);
        Assert.Equal("PROVIDER_TRANSIENT_FAILURE", firstFailure.FailureCode);
        Assert.True(firstFailure.CanRetry);
        Assert.Equal(1, firstFailure.AttemptCount);

        using var retryResponse = await RetryImportAsync(client, accepted.Id, "transient-retry");
        var retryAccepted = await ReadBatchAsync(retryResponse);
        var completed = await WaitForTerminalBatchAsync(client, accepted.Id);

        Assert.Equal(HttpStatusCode.Accepted, retryResponse.StatusCode);
        Assert.Equal(accepted.Id, retryAccepted.Id);
        Assert.Equal("Completed", completed.Status);
        Assert.Equal(2, completed.ImportedRows);
        Assert.Equal(2, completed.AttemptCount);

        using var replayResponse = await RetryImportAsync(client, accepted.Id, "transient-retry");
        var replay = await ReadBatchAsync(replayResponse);

        Assert.Equal(HttpStatusCode.Accepted, replayResponse.StatusCode);
        Assert.Equal(accepted.Id, replay.Id);
        Assert.Equal("Completed", replay.Status);
        Assert.Equal(2, factory.PayrollProvider.GetCallCount("transient"));
        Assert.Equal(2, await CountImportedEntriesAsync(factory, "TEST-IMPORT-"));
    }

    private static HttpClient TenantClient(CustomWebApplicationFactory factory, Guid organizationId)
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Organization-Id", organizationId.ToString());
        client.DefaultRequestHeaders.Add("X-User-Role", "PayrollManager");
        return client;
    }

    private static async Task<HttpResponseMessage> CreateImportAsync(
        HttpClient client,
        string scenario,
        string idempotencyKey)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            new Uri("/api/import-batches", UriKind.Relative))
        {
            Content = JsonContent.Create(new CreateImportBatchRequest(
                PeriodStart,
                PeriodEnd,
                scenario))
        };
        request.Headers.Add("Idempotency-Key", idempotencyKey);
        return await client.SendAsync(request);
    }

    private static async Task<HttpResponseMessage> RetryImportAsync(
        HttpClient client,
        Guid batchId,
        string idempotencyKey)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            new Uri($"/api/import-batches/{batchId}/retry", UriKind.Relative))
        {
            Content = JsonContent.Create(new { })
        };
        request.Headers.Add("Idempotency-Key", idempotencyKey);
        return await client.SendAsync(request);
    }

    private static async Task<ImportBatchView> ReadBatchAsync(HttpResponseMessage response) =>
        await response.Content.ReadFromJsonAsync<ImportBatchView>() ??
        throw new InvalidOperationException("The import response did not contain a batch.");

    private static async Task<List<ImportRowView>> GetErrorsAsync(HttpClient client, Guid batchId)
    {
        using var response = await client.GetAsync(
            new Uri($"/api/import-batches/{batchId}/errors", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return await response.Content.ReadFromJsonAsync<List<ImportRowView>>() ??
            throw new InvalidOperationException("The import error response did not contain a row collection.");
    }

    private static async Task<ImportBatchView> WaitForTerminalBatchAsync(
        HttpClient client,
        Guid batchId)
    {
        ImportBatchView? lastBatch = null;

        for (var attempt = 0; attempt < 120; attempt++)
        {
            using var response = await client.GetAsync(
                new Uri($"/api/import-batches/{batchId}", UriKind.Relative));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            lastBatch = await ReadBatchAsync(response);

            if (lastBatch.Status is not "Pending" and not "Processing")
            {
                return lastBatch;
            }

            var delayMilliseconds = Math.Min(10 + (attempt * 5), 50);
            await Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds));
        }

        throw new TimeoutException(
            $"Import batch {batchId} did not reach a terminal state. Last status: {lastBatch?.Status ?? "unknown"}.");
    }

    private static async Task<int> CountImportedEntriesAsync(
        CustomWebApplicationFactory factory,
        string externalIdPrefix)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await dbContext.TimeEntries
            .AsNoTracking()
            .CountAsync(entry =>
                entry.OrganizationId == DemoDataIds.OrganizationA &&
                entry.ExternalTimeEntryId.StartsWith(externalIdPrefix));
    }

    private static async Task<int> CountImportBatchesAsync(CustomWebApplicationFactory factory)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await dbContext.ImportBatches
            .AsNoTracking()
            .CountAsync(batch => batch.OrganizationId == DemoDataIds.OrganizationA);
    }

    private static async Task<List<string>> GetImportedEntryIdsAsync(
        CustomWebApplicationFactory factory,
        string externalIdPrefix)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await dbContext.TimeEntries
            .AsNoTracking()
            .Where(entry =>
                entry.OrganizationId == DemoDataIds.OrganizationA &&
                entry.ExternalTimeEntryId.StartsWith(externalIdPrefix))
            .OrderBy(entry => entry.ExternalTimeEntryId)
            .Select(entry => entry.ExternalTimeEntryId)
            .ToListAsync();
    }

    private static async Task SetEmployeeActiveFromAsync(
        CustomWebApplicationFactory factory,
        Guid employeeId,
        DateOnly activeFrom)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var employee = await dbContext.Employees.SingleAsync(value => value.Id == employeeId);
        employee.ActiveFrom = activeFrom;
        await dbContext.SaveChangesAsync();
    }

    private static async Task SetProjectStartsOnAsync(
        CustomWebApplicationFactory factory,
        Guid projectId,
        DateOnly startsOn)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var project = await dbContext.Projects.SingleAsync(value => value.Id == projectId);
        project.StartsOn = startsOn;
        await dbContext.SaveChangesAsync();
    }

    private static async Task SetProjectActiveAsync(
        CustomWebApplicationFactory factory,
        Guid projectId,
        bool isActive)
    {
        await using var scope = factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var project = await dbContext.Projects.SingleAsync(value => value.Id == projectId);
        project.IsActive = isActive;
        await dbContext.SaveChangesAsync();
    }
}
