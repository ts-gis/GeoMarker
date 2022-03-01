using Microsoft.AspNetCore.Mvc;

namespace GeoMarker.Controllers;

public class CheckTenantController : CustomControllerBase
{
    public CheckTenantController(IHttpContextAccessor accessor) : base(accessor)
    {

    }

    [HttpGet]
    public Task<string> GetTenant()
    {
        return Task.FromResult(Tenant);
    }
}