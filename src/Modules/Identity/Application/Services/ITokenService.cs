using System.Security.Claims;
using Cemiyet.Modules.Identity.Domain.Entities;
using Cemiyet.Modules.Identity.Domain.ValueObjects;

namespace Cemiyet.Modules.Identity.Application.Services;

public interface ITokenService
{
    AccessToken GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    ClaimsPrincipal? GetPrincipalFromToken(string token);
}

