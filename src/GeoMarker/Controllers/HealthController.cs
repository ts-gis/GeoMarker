using Microsoft.AspNetCore.Mvc;
using GeoMarker.Exceptions;

namespace GeoMarker.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Home()
    {
       return Redirect("/swagger/index.html");
    }

    [HttpGet]
    public Task<string> Health()
    {
        return Task.FromResult("Health");
    }

    [HttpGet("business-exception")]
    public Task ThrowBusinessException()
    {
        throw new BusinessException(401, "认证失败");
    }

    [HttpGet("system-exception")]
    public Task ThrowSystemException()
    {
        throw new Exception("系统异常");
    }
}