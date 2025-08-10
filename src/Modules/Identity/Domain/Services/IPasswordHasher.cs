namespace Cemiyet.Modules.Identity.Domain.Services;

/// <summary>
/// Interface for password hashing services.
/// Provides methods to hash passwords and verify them against stored hashes.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a plain text password using a secure hashing algorithm.
    /// The hash can be stored securely and used for password verification later.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed password as a string.</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies a plain text password against a stored hash.
    /// This method checks if the provided password matches the hash.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="hash">The stored hash to compare against.</param>
    /// <returns>True if the password matches the hash, otherwise false.</returns>
    /// <remarks>
    /// This method is used to authenticate users by checking their input against the stored password hash.
    /// </remarks>
    bool Verify(string password, string hash);
}
