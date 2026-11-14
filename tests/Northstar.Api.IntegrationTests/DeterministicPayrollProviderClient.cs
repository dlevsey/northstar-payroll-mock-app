using System.Collections.Concurrent;
using Northstar.Application.Abstractions;

namespace Northstar.Api.IntegrationTests;

public sealed class DeterministicPayrollProviderClient : IPayrollProviderClient
{
    private readonly ConcurrentDictionary<string, int> _scenarioCalls =
        new(StringComparer.OrdinalIgnoreCase);
    private int _returnCaseVariantOnNextSuccess;

    public int GetCallCount(string scenario) =>
        _scenarioCalls.TryGetValue(scenario, out var count) ? count : 0;

    public void ReturnCaseVariantOnNextSuccess() =>
        Interlocked.Exchange(ref _returnCaseVariantOnNextSuccess, 1);

    public Task<IReadOnlyList<ProviderTimeEntryRecord>> GetTimeEntriesAsync(
        ProviderImportRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var scenario = request.Scenario.Trim().ToLowerInvariant();
        var attempt = _scenarioCalls.AddOrUpdate(scenario, 1, static (_, current) => current + 1);

        return scenario switch
        {
            "success" => SuccessAsync(
                request,
                Interlocked.Exchange(ref _returnCaseVariantOnNextSuccess, 0) == 1),
            "mixed" => MixedAsync(request),
            "transient" when attempt == 1 => Task.FromException<IReadOnlyList<ProviderTimeEntryRecord>>(
                new PayrollProviderTransientException("Synthetic transient provider failure.")),
            "transient" => SuccessAsync(request, useCaseVariant: false),
            "timeout" => Task.FromException<IReadOnlyList<ProviderTimeEntryRecord>>(
                new PayrollProviderTransientException("Synthetic provider timeout.")),
            "malformed" => Task.FromException<IReadOnlyList<ProviderTimeEntryRecord>>(
                new PayrollProviderContractException("The synthetic provider returned malformed JSON.")),
            "permanent" => Task.FromException<IReadOnlyList<ProviderTimeEntryRecord>>(
                new PayrollProviderContractException("The synthetic provider rejected the request.")),
            _ => Task.FromException<IReadOnlyList<ProviderTimeEntryRecord>>(
                new PayrollProviderContractException($"Unsupported test provider scenario '{scenario}'."))
        };
    }

    private static Task<IReadOnlyList<ProviderTimeEntryRecord>> SuccessAsync(
        ProviderImportRequest request,
        bool useCaseVariant) =>
        Task.FromResult<IReadOnlyList<ProviderTimeEntryRecord>>(
        [
            new(
                useCaseVariant ? "test-import-001" : "TEST-IMPORT-001",
                "A-EMP-001",
                "A-PRJ-001",
                "ELEC",
                request.PeriodStart,
                8m,
                0m,
                "DAY"),
            new(
                useCaseVariant ? "test-import-002" : "TEST-IMPORT-002",
                "A-EMP-002",
                "A-PRJ-001",
                "LAB",
                NextWorkDate(request),
                7.5m,
                1m,
                "DAY")
        ]);

    private static Task<IReadOnlyList<ProviderTimeEntryRecord>> MixedAsync(
        ProviderImportRequest request) =>
        Task.FromResult<IReadOnlyList<ProviderTimeEntryRecord>>(
        [
            new(
                "TEST-MIXED-001",
                "A-EMP-001",
                "A-PRJ-001",
                "ELEC",
                request.PeriodStart,
                8m,
                0m,
                "DAY"),
            new(
                "TEST-MIXED-001",
                "A-EMP-002",
                "A-PRJ-001",
                "LAB",
                NextWorkDate(request),
                4m,
                0m,
                "DAY"),
            new(
                "TEST-MIXED-UNKNOWN-EMPLOYEE",
                "A-EMP-999",
                "A-PRJ-001",
                "ELEC",
                request.PeriodStart,
                8m,
                0m,
                "DAY"),
            new(
                "TEST-MIXED-NEGATIVE-HOURS",
                "A-EMP-001",
                "A-PRJ-001",
                "ELEC",
                NextWorkDate(request),
                -1m,
                0m,
                "NIGHT")
        ]);

    private static DateOnly NextWorkDate(ProviderImportRequest request) =>
        request.PeriodStart < request.PeriodEnd
            ? request.PeriodStart.AddDays(1)
            : request.PeriodStart;
}
