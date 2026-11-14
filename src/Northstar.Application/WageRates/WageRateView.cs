using Northstar.Domain.WageRates;

namespace Northstar.Application.WageRates;

public sealed record CreateWageRateRequest(
    WageRateScope Scope,
    string JurisdictionCode,
    Guid? ProjectId,
    Guid? EmployeeId,
    Guid ClassificationId,
    DateOnly EffectiveFrom,
    DateOnly? EffectiveTo,
    decimal BaseRate,
    decimal FringeRate,
    decimal OvertimeMultiplier,
    int Priority,
    string SourceReference);

public sealed record WageRateView(
    Guid Id,
    string Scope,
    string JurisdictionCode,
    Guid? ProjectId,
    Guid? EmployeeId,
    Guid ClassificationId,
    DateOnly EffectiveFrom,
    DateOnly? EffectiveTo,
    decimal BaseRate,
    decimal FringeRate,
    decimal OvertimeMultiplier,
    int Priority,
    string SourceReference);
