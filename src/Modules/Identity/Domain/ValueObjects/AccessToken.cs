using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.ValueObjects;

public sealed class AccessToken : ValueObject
{
    public string Token { get; }
    public DateTime ExpiresAt { get; }

    public AccessToken(string token, DateTime expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Token;
        yield return ExpiresAt;
    }
}


