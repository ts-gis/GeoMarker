using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GeoMarker.Infrastucture.Exceptions;

namespace GeoMarker.Infrastucture.Filters;

public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;
        var result = new ObjectResult("");

        if (exception is BusinessException ex0)
        {
            result.StatusCode = 200;
            result.Value = new { ex0.Code, ex0.Message };
        }
        else if (exception is ServerException ex1)
        {
            result.StatusCode = 500;
            result.Value = ex1.Message;
        }
        else
        {
            result.StatusCode = 500;
            result.Value = "服务器异常";
        }

        context.Result = result;

        return Task.CompletedTask;
    }
}
