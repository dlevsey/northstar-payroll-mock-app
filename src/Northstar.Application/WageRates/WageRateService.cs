using Microsoft.EntityFrameworkCore;
using Northstar.Application.Abstractions;
using Northstar.Application.Common;
using Northstar.Domain.WageRates;

namespace Northstar.Application.WageRates;

public sealed class WageRateService(IApplicationDbContext dbContext)
{
    public async Task<IReadOnlyList<WageRateView>> ListAsync(
        Guid organizationId,
        CancellationToken cancellationToken) =>
        await dbContext.WageRates
            .AsNoTracking()
            .Where(rate => rate.OrganizationId == organizationId)
            .OrderBy(rate => rate.ClassificationId)
            .ThenBy(rate => rate.Scope)
            .ThenBy(rate => rate.EffectiveFrom)
            .Select(rate => Map(rate))
            .ToListAsync(cancellationToken);

    public async Task<OperationResult<WageRateView>> CreateAsync(
        Guid organizationId,
        CreateWageRateRequest request,
        CancellationToken cancellationToken)
    {
        if (request.EffectiveTo < request.EffectiveFrom)
        {
            return OperationResult<WageRateView>.Invalid(
                "INVALID_EFFECTIVE_RANGE",
                "EffectiveTo cannot be before EffectiveFrom.");
        }

        if (request.BaseRate < 0 || request.FringeRate < 0 || request.OvertimeMultiplier < 1)
        {
            return OperationResult<WageRateView>.Invalid(
                "INVALID_RATE_AMOUNT",
                "Rates must be nonnegative and the overtime multiplier must be at least one.");
        }

        var classificationExists = await dbContext.WorkClassifications
            .AnyAsync(
                classification => classification.OrganizationId == organizationId &&
                    classification.Id == request.ClassificationId,
                cancellationToken);

        if (!classificationExists)
        {
            return OperationResult<WageRateView>.Invalid(
                "CLASSIFICATION_NOT_FOUND",
                "The classification does not belong to the current organization.");
        }

        if (request.ProjectId is not null && !await dbContext.Projects.AnyAsync(
                project => project.OrganizationId == organizationId && project.Id == request.ProjectId,
                cancellationToken))
        {
            return OperationResult<WageRateView>.Invalid(
                "PROJECT_NOT_FOUND",
                "The project does not belong to the current organization.");
        }

        if (request.EmployeeId is not null && !await dbContext.Employees.AnyAsync(
                employee => employee.OrganizationId == organizationId && employee.Id == request.EmployeeId,
                cancellationToken))
        {
            return OperationResult<WageRateView>.Invalid(
                "EMPLOYEE_NOT_FOUND",
                "The employee does not belong to the current organization.");
        }

        var overlaps = await dbContext.WageRates.AnyAsync(
            rate => rate.OrganizationId == organizationId &&
                rate.ClassificationId == request.ClassificationId &&
                rate.Scope == request.Scope &&
                rate.Priority == request.Priority &&
                rate.ProjectId == request.ProjectId &&
                rate.EmployeeId == request.EmployeeId &&
                rate.EffectiveFrom <= (request.EffectiveTo ?? DateOnly.MaxValue) &&
                (rate.EffectiveTo == null || rate.EffectiveTo >= request.EffectiveFrom),
            cancellationToken);

        if (overlaps)
        {
            return OperationResult<WageRateView>.Invalid(
                "RATE_OVERLAP",
                "An equally ranked rate already overlaps this effective period.");
        }

        var rate = new WageRate
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            Scope = request.Scope,
            JurisdictionCode = request.JurisdictionCode.Trim().ToUpperInvariant(),
            ProjectId = request.ProjectId,
            EmployeeId = request.EmployeeId,
            ClassificationId = request.ClassificationId,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
            BaseRate = request.BaseRate,
            FringeRate = request.FringeRate,
            OvertimeMultiplier = request.OvertimeMultiplier,
            Priority = request.Priority,
            SourceReference = request.SourceReference.Trim(),
            Version = 1
        };

        dbContext.Add(rate);
        await dbContext.SaveChangesAsync(cancellationToken);
        return OperationResult<WageRateView>.Success(Map(rate));
    }

    private static WageRateView Map(WageRate rate) =>
        new(
            rate.Id,
            rate.Scope.ToString(),
            rate.JurisdictionCode,
            rate.ProjectId,
            rate.EmployeeId,
            rate.ClassificationId,
            rate.EffectiveFrom,
            rate.EffectiveTo,
            rate.BaseRate,
            rate.FringeRate,
            rate.OvertimeMultiplier,
            rate.Priority,
            rate.SourceReference);
}
