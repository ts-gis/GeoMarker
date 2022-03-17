using Microsoft.AspNetCore.Mvc;
using GeoMarker.Infrastucture.Exceptions;

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
    public Task ThrowBusinessException(int type)
    {
        throw type switch
        {
            0 => new BusinessException(2222, "认证失败"),
            1 => new ServerException("代码配置出现问题"),
            _ => new Exception("系统异常"),
        };
    }
}