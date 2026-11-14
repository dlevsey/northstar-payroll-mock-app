using Microsoft.AspNetCore.Mvc;
using Northstar.Api.Security;
using Northstar.Application.Common;

namespace Northstar.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase(HeaderUserContext userContext) : ControllerBase
{
    protected bool TryGetOrganizationId(out Guid organizationId) =>
        userContext.TryGetOrganizationId(out organizationId);

    protected bool IsPayrollManager => userContext.IsPayrollManager;

    protected ActionResult<T> ToActionResult<T>(OperationResult<T> result) =>
        result.Outcome switch
        {
            OperationOutcome.Success => Ok(result.Value),
            OperationOutcome.NotFound => NotFound(CreateProblem(
                StatusCodes.Status404NotFound,
                "Resource not found")),
            OperationOutcome.Conflict => Conflict(CreateProblem(
                StatusCodes.Status409Conflict,
                result.ErrorCode ?? "Conflict",
                result.ErrorMessage)),
            OperationOutcome.Invalid => UnprocessableEntity(CreateProblem(
                StatusCodes.Status422UnprocessableEntity,
                result.ErrorCode ?? "Invalid request",
                result.ErrorMessage)),
            OperationOutcome.Forbidden => StatusCode(StatusCodes.Status403Forbidden),
            _ => throw new InvalidOperationException($"Unsupported outcome {result.Outcome}.")
        };

    protected ActionResult MissingTenantHeader() => Unauthorized(CreateProblem(
        StatusCodes.Status401Unauthorized,
        "Organization context required",
        "Provide a valid X-Organization-Id header for the local study authentication scheme."));

    protected ActionResult ForbiddenForLocalStudyUser() =>
        StatusCode(StatusCodes.Status403Forbidden);

    private ProblemDetails CreateProblem(int status, string title, string? detail = null) =>
        new()
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = HttpContext.Request.Path
        };
}
