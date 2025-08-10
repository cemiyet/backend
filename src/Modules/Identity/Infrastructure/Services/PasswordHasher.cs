using Cemiyet.Modules.Identity.Domain.Services;

namespace Cemiyet.Modules.Identity.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        // TODO: handle domain errors better (e.g., throw specific exceptions or use result objects)
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        // TODO: Validate password strength
        // TODO: Replace with secure hash implementation (e.g., BCrypt)

        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public bool Verify(string password, string hash)
    {
        return Hash(password) == hash;
    }
}