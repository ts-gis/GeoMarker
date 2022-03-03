using Microsoft.AspNetCore.Mvc;
using GeoMarker.Exceptions;

namespace GeoMarker.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// 跳转到swagger文档
    /// </summary>
    /// <returns></returns>
    [HttpGet("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Home()
    {
        return Redirect("/swagger/index.html");
    }

    /// <summary>
    /// 验证租户正确性
    /// </summary>
    /// <param name="tenant"></param>
    /// <returns></returns>
    [HttpGet("/{tenant}/health")]
    public Task<string> ValidateTenant(string tenant) => Task.FromResult(tenant);

    /// <summary>
    /// 简单的health
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<string> Health()
    {
        return Task.FromResult("Health");
    }

    /// <summary>
    /// 验证抛出业务异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    [HttpGet("business-exception")]
    public Task ThrowBusinessException()
    {
        throw new BusinessException(401, "认证失败");
    }

    /// <summary>
    /// 验证抛出系统异常
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("system-exception")]
    public Task ThrowSystemException()
    {
        throw new Exception("系统异常");
    }
}