using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Services;

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int TenantId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?
                .User
                .FindFirst("tenantId");

            if (claim == null)
            {
                throw new UnauthorizedAccessException(
                    "Tenant claim not found.");
            }

            return int.Parse(claim.Value);
        }
    }
}