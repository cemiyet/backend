using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.Entities;

public sealed class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string? DisplayName { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { } // for EF Core

    public static User Register(Email email, string passwordHash, string? displayName = null)
    {
        // TODO: check if email is already registered
        // TODO: displayName validation (e.g., length, allowed characters)
        // TODO: validate password strength

        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash,
            DisplayName = displayName,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void ConfirmEmail() => EmailConfirmed = true;
    public void UpdateDisplayName(string name) => DisplayName = name;
}