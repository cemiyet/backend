namespace Cemiyet.Modules.Identity.Infrastructure.Authentication;

public sealed class JwtSettings
{
    public string SecretKey { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int AccessTokenLifetimeMinutes { get; init; }
    public int RefreshTokenLifetimeDays { get; init; }
}

