using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GeoMarker.Exceptions;

namespace GeoMarker.Filters;

public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;
        var result = new ObjectResult("");

        if (exception is BusinessException ex)
        {
            result.StatusCode = ex.Code;
            result.Value = ex.Message;
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
