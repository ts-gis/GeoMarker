namespace GeoMarker.Services
{
    public interface ITenantService
    {
        string TenantName { get; }
    }

    public class TenantService : ITenantService
    {
        private string? _tenantName;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string TenantName => _tenantName ??= httpContextAccessor.HttpContext.GetRouteValue("tenant")?.ToString();
    }
}
