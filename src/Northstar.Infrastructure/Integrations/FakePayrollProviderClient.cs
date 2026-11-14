using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Northstar.Application.Abstractions;

namespace Northstar.Infrastructure.Integrations;

public sealed class FakePayrollProviderClient(
    HttpClient httpClient,
    IOptions<PayrollProviderOptions> options) : IPayrollProviderClient
{
    private readonly PayrollProviderOptions _options = options.Value;

    public async Task<IReadOnlyList<ProviderTimeEntryRecord>> GetTimeEntriesAsync(
        ProviderImportRequest request,
        CancellationToken cancellationToken)
    {
        var records = new List<ProviderTimeEntryRecord>();
        var observedCursors = new HashSet<string>(StringComparer.Ordinal);
        string? cursor = null;

        for (var pageNumber = 0; pageNumber < 100; pageNumber++)
        {
            var page = await GetPageWithRetryAsync(request, cursor, cancellationToken);

            if (page?.Records is null)
            {
                throw new PayrollProviderContractException(
                    "The payroll provider response did not contain a records collection.");
            }

            foreach (var record in page.Records)
            {
                if (record is null)
                {
                    throw new PayrollProviderContractException(
                        "The payroll provider response contained a null record.");
                }

                records.Add(new ProviderTimeEntryRecord(
                    record.ExternalTimeEntryId,
                    record.ExternalEmployeeId,
                    record.ExternalProjectId,
                    record.ClassificationCode,
                    record.WorkDate,
                    record.RegularHours,
                    record.OvertimeHours,
                    record.ShiftCode));
            }

            if (string.IsNullOrWhiteSpace(page.NextCursor))
            {
                return records;
            }

            if (!observedCursors.Add(page.NextCursor))
            {
                throw new PayrollProviderContractException(
                    "The payroll provider repeated a pagination cursor.");
            }

            cursor = page.NextCursor;
        }

        throw new PayrollProviderContractException(
            "The payroll provider exceeded the 100-page import safety limit.");
    }

    private async Task<ProviderTimeEntryPage?> GetPageWithRetryAsync(
        ProviderImportRequest request,
        string? cursor,
        CancellationToken cancellationToken)
    {
        Exception? lastException = null;

        for (var attempt = 1; attempt <= _options.MaxAttempts; attempt++)
        {
            TimeSpan? retryDelay = null;
            using var attemptTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            attemptTimeout.CancelAfter(TimeSpan.FromMilliseconds(_options.AttemptTimeoutMilliseconds));

            try
            {
                var uri = BuildUri(request, cursor);
                using var response = await httpClient.GetAsync(
                    uri,
                    HttpCompletionOption.ResponseHeadersRead,
                    attemptTimeout.Token);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        return await response.Content.ReadFromJsonAsync<ProviderTimeEntryPage>(
                            cancellationToken: attemptTimeout.Token);
                    }
                    catch (JsonException exception)
                    {
                        throw new PayrollProviderContractException(
                            "The payroll provider returned malformed JSON.",
                            exception);
                    }
                }

                if (!IsTransient(response.StatusCode))
                {
                    var statusCode = (int)response.StatusCode;
                    throw new PayrollProviderContractException(
                        $"The payroll provider rejected the import request with HTTP {statusCode}.");
                }

                if (attempt == _options.MaxAttempts)
                {
                    var statusCode = (int)response.StatusCode;
                    throw new PayrollProviderTransientException(
                        $"The payroll provider remained unavailable after {_options.MaxAttempts} attempts (HTTP {statusCode}).");
                }

                retryDelay = GetRetryDelay(response, attempt);
            }
            catch (HttpRequestException exception) when (attempt < _options.MaxAttempts)
            {
                lastException = exception;
                retryDelay = GetBackoff(attempt);
            }
            catch (HttpRequestException exception)
            {
                throw new PayrollProviderTransientException(
                    $"The payroll provider could not be reached after {_options.MaxAttempts} attempts.",
                    exception);
            }
            catch (OperationCanceledException exception)
                when (!cancellationToken.IsCancellationRequested && attempt < _options.MaxAttempts)
            {
                lastException = exception;
                retryDelay = GetBackoff(attempt);
            }
            catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)
            {
                throw new PayrollProviderTransientException(
                    $"The payroll provider timed out after {_options.MaxAttempts} attempts.",
                    exception);
            }

            await Task.Delay(retryDelay!.Value, cancellationToken);
        }

        throw new PayrollProviderTransientException(
            "The payroll provider could not be reached.",
            lastException);
    }

    private string BuildUri(ProviderImportRequest request, string? cursor)
    {
        var values = new Dictionary<string, string?>
        {
            ["organizationReference"] = request.OrganizationReference,
            ["periodStart"] = request.PeriodStart.ToString("yyyy-MM-dd"),
            ["periodEnd"] = request.PeriodEnd.ToString("yyyy-MM-dd"),
            ["scenario"] = request.Scenario,
            ["cursor"] = cursor
        };
        var query = string.Join(
            "&",
            values
                .Where(pair => pair.Value is not null)
                .Select(pair => $"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value!)}"));
        return $"api/time-entries?{query}";
    }

    private TimeSpan GetRetryDelay(HttpResponseMessage response, int attempt)
    {
        var retryAfter = response.Headers.RetryAfter?.Delta;
        return retryAfter is not null
            ? TimeSpan.FromMilliseconds(Math.Min(retryAfter.Value.TotalMilliseconds, 1_000))
            : GetBackoff(attempt);
    }

    private TimeSpan GetBackoff(int attempt) =>
        TimeSpan.FromMilliseconds(_options.BaseRetryDelayMilliseconds * Math.Pow(2, attempt - 1));

    private static bool IsTransient(HttpStatusCode statusCode) =>
        statusCode is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests ||
        (int)statusCode >= 500;

    private sealed record ProviderTimeEntryPage(
        IReadOnlyList<ProviderTimeEntryDto?>? Records,
        string? NextCursor);

    private sealed record ProviderTimeEntryDto(
        string? ExternalTimeEntryId,
        string? ExternalEmployeeId,
        string? ExternalProjectId,
        string? ClassificationCode,
        DateOnly? WorkDate,
        decimal? RegularHours,
        decimal? OvertimeHours,
        string? ShiftCode);
}
