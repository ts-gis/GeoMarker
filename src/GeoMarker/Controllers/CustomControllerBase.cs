using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoMarker.Controllers;

[Authorize]
[ApiController]
[Route($"api/[controller]", Name = "[controller]")]
public class CustomControllerBase : ControllerBase
{

    public CustomControllerBase()
    {
    }

    [NonAction]
    public new OkObjectResult Ok(object value)
    {
        return base.Ok(new
        {
            Code = 0,
            Data = value
        });
    }
}