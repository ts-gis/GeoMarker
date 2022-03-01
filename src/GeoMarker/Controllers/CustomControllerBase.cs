using Microsoft.AspNetCore.Mvc;

namespace GeoMarker.Controllers;

[ApiController]
[Route("api/{tenant}/[controller]",Name = "[controller]")]
public class CustomControllerBase : ControllerBase
{
    public string Tenant { get; }

    public CustomControllerBase(IHttpContextAccessor accessor)
    {
        Tenant = accessor.HttpContext.GetRouteValue("tenant").ToString();
    }
}