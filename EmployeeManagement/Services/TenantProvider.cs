using EmployeeManagement.Interfaces;

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public int TenantId =>
        int.Parse(
            _httpContextAccessor
                .HttpContext!
                .User
                .FindFirst("tenantId")!
                .Value);
}