using System.Security.Claims;

using GeoMarker.Infrastucture.Exceptions;

namespace GeoMarker.Services
{
    public interface ITenantInfo
    {
        string Name { get; }
    }

    public class TenantInfo : ITenantInfo
    {   
        private readonly HttpContext httpContext;
        private readonly IJwtService jwtService;

        public TenantInfo(IHttpContextAccessor httpContextAccessor,IJwtService jwtService)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.jwtService = jwtService;
        }

        public string Name
        {
            get
            {
                if (!httpContext.Request.Headers.TryGetValue("Authorization", out var token))
                {
                    httpContext.Request.Query.TryGetValue("access_token", out token);
                }

                token = token.ToString().Split(' ').Last();
                var claims = jwtService.ResolveToken(token);
                var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(name))
                    throw new ServerException("token中不包含 NameIdentifier claim type");

                return name;
            }
        }
    }

    public class TenantInfoForDev : ITenantInfo
    {
        public string Name => "tracy";
    }
}
