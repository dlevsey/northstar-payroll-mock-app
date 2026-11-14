namespace Northstar.Application.Abstractions;

public sealed record ProviderImportRequest(
    string OrganizationReference,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    string Scenario);

public sealed record ProviderTimeEntryRecord(
    string? ExternalTimeEntryId,
    string? ExternalEmployeeId,
    string? ExternalProjectId,
    string? ClassificationCode,
    DateOnly? WorkDate,
    decimal? RegularHours,
    decimal? OvertimeHours,
    string? ShiftCode);

public interface IPayrollProviderClient
{
    Task<IReadOnlyList<ProviderTimeEntryRecord>> GetTimeEntriesAsync(
        ProviderImportRequest request,
        CancellationToken cancellationToken);
}

public abstract class PayrollProviderException(string message, Exception? innerException = null)
    : Exception(message, innerException);

public sealed class PayrollProviderTransientException(string message, Exception? innerException = null)
    : PayrollProviderException(message, innerException);

public sealed class PayrollProviderContractException(string message, Exception? innerException = null)
    : PayrollProviderException(message, innerException);
