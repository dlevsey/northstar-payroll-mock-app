namespace Northstar.Api.Security;

public sealed class HeaderUserContext(IHttpContextAccessor httpContextAccessor)
{
    public bool TryGetOrganizationId(out Guid organizationId)
    {
        var value = httpContextAccessor.HttpContext?.Request.Headers["X-Organization-Id"].ToString();
        return Guid.TryParse(value, out organizationId);
    }

    public bool IsPayrollManager => string.Equals(
        httpContextAccessor.HttpContext?.Request.Headers["X-User-Role"].ToString(),
        "PayrollManager",
        StringComparison.OrdinalIgnoreCase);
}
