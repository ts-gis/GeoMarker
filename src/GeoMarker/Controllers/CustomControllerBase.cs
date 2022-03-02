using Microsoft.AspNetCore.Mvc;

namespace GeoMarker.Controllers;

[ApiController]
[Route("api/{tenant}/[controller]",Name = "[controller]")]
public class CustomControllerBase : ControllerBase
{
    public CustomControllerBase()
    {
    }
}