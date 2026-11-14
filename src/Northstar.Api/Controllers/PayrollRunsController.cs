using Microsoft.AspNetCore.Mvc;
using Northstar.Api.Security;
using Northstar.Application.PayrollRuns;

namespace Northstar.Api.Controllers;

[Route("api/payroll-runs")]
public sealed class PayrollRunsController(
    HeaderUserContext userContext,
    PayrollRunService payrollRunService) : ApiControllerBase(userContext)
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<PayrollRunView>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PayrollRunView>>> List(
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return Ok(await payrollRunService.ListAsync(organizationId, cancellationToken));
    }

    [HttpGet("{runId:guid}")]
    [ProducesResponseType<PayrollRunView>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PayrollRunView>> Get(
        Guid runId,
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return ToActionResult(await payrollRunService.GetAsync(
            organizationId,
            runId,
            cancellationToken));
    }

    [HttpPost("{runId:guid}/calculate")]
    [ProducesResponseType<PayrollRunView>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<PayrollRunView>> Calculate(
        Guid runId,
        [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey,
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return ToActionResult(await payrollRunService.CalculateAsync(
            organizationId,
            runId,
            idempotencyKey ?? string.Empty,
            cancellationToken));
    }

    [HttpPost("{runId:guid}/approve")]
    [ProducesResponseType<PayrollRunView>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PayrollRunView>> Approve(
        Guid runId,
        ApprovePayrollRunRequest request,
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

        return ToActionResult(await payrollRunService.ApproveAsync(
            organizationId,
            runId,
            request.ExpectedVersion,
            cancellationToken));
    }
}

public sealed record ApprovePayrollRunRequest(long ExpectedVersion);
