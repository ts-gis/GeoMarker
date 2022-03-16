using GeoMarker.Infrastucture.Exceptions;
using System.Security.Claims;

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

                if (string.IsNullOrWhiteSpace(token))
                    throw new BusinessException(400, "无法获取认证token");

                token = token.ToString().Split(' ').Last();
                var claims = jwtService.ResolveToken(token);

                if (claims == null)
                    throw new BusinessException(400, "解析token异常");

                var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(name))
                    throw new BusinessException(400, "token中不包含id claims");

                return name;
            }
        }
    }

    public class TenantInfoForDev : ITenantInfo
    {
        public string Name => "tracy";
    }
}
