using GeoMarker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GeoMarker.Filters
{
    public class TenantValidateFilter : IAsyncActionFilter
    {
        private readonly ITenantService tenantService;
        private readonly IConfiguration configuration;

        public TenantValidateFilter(ITenantService tenantService,IConfiguration configuration)
        {
            this.tenantService = tenantService;
            this.configuration = configuration;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.RouteValues.TryGetValue(Contract.TENANT, out var tenant);

            if(tenant == null)
                return next();

            var section = configuration.GetSection($"{Contract.TENANT}:{tenant}");
            if (section.Exists())
            {
                tenantService.TenantName = tenant.ToString();
                section.Bind(tenantService.Info);

                return next();
            }

            context.Result = new BadRequestObjectResult($"tenant ：{tenant} 不存在!");
            return Task.CompletedTask;
        }
    }
}
