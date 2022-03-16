using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoMarker.Controllers;

[ApiController]
[Route($"api/[controller]", Name = "[controller]")]
public class CustomControllerBase : ControllerBase
{

    public CustomControllerBase()
    {
    }
}