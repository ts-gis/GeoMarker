using Microsoft.AspNetCore.Mvc;

namespace GeoMarker.Controllers;

[ApiController]
[Route($"api/{{{Contract.TENANT}}}/[controller]",Name = "[controller]")]
public class CustomControllerBase : ControllerBase
{
    public CustomControllerBase()
    {
    }
}