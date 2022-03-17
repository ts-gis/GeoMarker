using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using GeoMarker.Infrastucture.Configuration;

namespace GeoMarker.Services;

public interface IJwtService
{
    string GenerateToken(IEnumerable<Claim> claims);

    IEnumerable<Claim>? ResolveToken(string token);
}

public class JwtService : IJwtService
{
    private readonly JwtConfig jwtConfig;

    public JwtService(IOptions<JwtConfig> configuration)
    {
        jwtConfig = configuration.Value;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
        issuer: jwtConfig.Issuer,
        audience: jwtConfig.Audience,
        expires: DateTime.Now.AddDays(jwtConfig.Expire),
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(jwtConfig.KeyBytes),
            SecurityAlgorithms.HmacSha256),
        claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IEnumerable<Claim>? ResolveToken(string token) =>
        new JwtSecurityTokenHandler().ReadJwtToken(token)?.Claims;
}
