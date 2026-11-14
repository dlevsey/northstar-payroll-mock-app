using Microsoft.AspNetCore.Mvc;
using Northstar.Api.Security;
using Northstar.Application.Common;
using Northstar.Application.Imports;

namespace Northstar.Api.Controllers;

[Route("api/import-batches")]
public sealed class ImportBatchesController(
    HeaderUserContext userContext,
    ImportBatchService importBatchService) : ApiControllerBase(userContext)
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<ImportBatchView>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ImportBatchView>>> List(
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return Ok(await importBatchService.ListAsync(organizationId, cancellationToken));
    }

    [HttpGet("{batchId:guid}")]
    [ProducesResponseType<ImportBatchView>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ImportBatchView>> Get(
        Guid batchId,
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return ToActionResult(await importBatchService.GetAsync(
            organizationId,
            batchId,
            cancellationToken));
    }

    [HttpGet("{batchId:guid}/errors")]
    [ProducesResponseType<IReadOnlyList<ImportRowView>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ImportRowView>>> GetErrors(
        Guid batchId,
        CancellationToken cancellationToken)
    {
        if (!TryGetOrganizationId(out var organizationId))
        {
            return MissingTenantHeader();
        }

        return ToActionResult(await importBatchService.GetErrorsAsync(
            organizationId,
            batchId,
            cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType<ImportBatchView>(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ImportBatchView>> Create(
        CreateImportBatchRequest request,
        [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey,
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

        var result = await importBatchService.CreateAsync(
            organizationId,
            request,
            idempotencyKey ?? string.Empty,
            cancellationToken);
        return ToAcceptedResult(result);
    }

    [HttpPost("{batchId:guid}/retry")]
    [ProducesResponseType<ImportBatchView>(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ImportBatchView>> Retry(
        Guid batchId,
        [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey,
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

        var result = await importBatchService.RetryAsync(
            organizationId,
            batchId,
            idempotencyKey ?? string.Empty,
            cancellationToken);
        return ToAcceptedResult(result);
    }

    private ActionResult<ImportBatchView> ToAcceptedResult(
        OperationResult<ImportBatchView> result) =>
        result.Outcome == OperationOutcome.Success
            ? AcceptedAtAction(nameof(Get), new { batchId = result.Value!.Id }, result.Value)
            : ToActionResult(result);
}
