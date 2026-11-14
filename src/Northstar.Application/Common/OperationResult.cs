namespace Northstar.Application.Common;

public enum OperationOutcome
{
    Success,
    NotFound,
    Conflict,
    Invalid,
    Forbidden
}

public sealed record OperationResult<T>(
    OperationOutcome Outcome,
    T? Value = default,
    string? ErrorCode = null,
    string? ErrorMessage = null)
{
    public static OperationResult<T> Success(T value) => new(OperationOutcome.Success, value);

    public static OperationResult<T> NotFound() => new(OperationOutcome.NotFound);

    public static OperationResult<T> Conflict(string code, string message) =>
        new(OperationOutcome.Conflict, default, code, message);

    public static OperationResult<T> Invalid(string code, string message) =>
        new(OperationOutcome.Invalid, default, code, message);

    public static OperationResult<T> Forbidden() => new(OperationOutcome.Forbidden);
}
