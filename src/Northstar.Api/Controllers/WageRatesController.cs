using Microsoft.AspNetCore.Mvc;
using Northstar.Api.Security;
using Northstar.Application.WageRates;

namespace Northstar.Api.Controllers;

[Route("api/wage-rates")]
public sealed class WageRatesController(
    HeaderUserContext userContext,
    WageRateService wageRateService) : ApiControllerBase(userContext)
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<WageRateView>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<WageRateView>>> List(
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return Ok(await wageRateService.ListAsync(organizationId, cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType<WageRateView>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<WageRateView>> Create(
        CreateWageRateRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        if (!IsPayrollManager)
        {
            return ForbiddenForLocalStudyUser();
        }

        var result = await wageRateService.CreateAsync(organizationId, request, cancellationToken);
        if (result.Outcome != Application.Common.OperationOutcome.Success)
        {
            return ToActionResult(result);
        }

        return Created($"/api/wage-rates/{result.Value!.Id}", result.Value);
    }
}
