using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cemiyet.Modules.Identity.Application.Services;
using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cemiyet.Modules.Identity.Infrastructure.Authentication;

internal sealed class TokenService : ITokenService
{
    private readonly JwtSettings _settings;
    private readonly SymmetricSecurityKey _signingKey;
    private readonly SigningCredentials _signingCredentials;

    public TokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        _signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
    }

    public AccessToken GenerateAccessToken(User user)
    {
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Address)
            ],
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenLifetimeMinutes),
            signingCredentials: _signingCredentials
        );

        return new AccessToken(new JwtSecurityTokenHandler().WriteToken(token), DateTime.UtcNow.AddMinutes(_settings.AccessTokenLifetimeMinutes));
    }

    public string GenerateRefreshToken(User user)
    {
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims:
            [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Address)
            ],
            expires: DateTime.UtcNow.AddDays(_settings.RefreshTokenLifetimeDays),
            signingCredentials: _signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        try
        {
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuerSigningKey = true
            }, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtToken
                || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
