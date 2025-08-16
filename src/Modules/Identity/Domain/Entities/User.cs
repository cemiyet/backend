using Cemiyet.Modules.Identity.Domain.Events;
using Cemiyet.Modules.Identity.Domain.Services;
using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.Entities;

public sealed class User : AggregateRoot<Guid>
{
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string? DisplayName { get; private set; }
    public bool EmailConfirmed { get; private set; }

    private User() { } // for EF Core

    public static User Register(Email email, string passwordHash, string? displayName = null)
    {
        // TODO: displayName validation (e.g., length, allowed characters)

        // TODO: handle domain errors better (e.g., throw specific exceptions or use result objects)
        ArgumentNullException.ThrowIfNull(email);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash,
            DisplayName = displayName,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        user.AddDomainEvent(new UserRegistered(user.Id, user.Email));

        return user;
    }

    public void ChangePassword(IPasswordHasher hasher, string newPassword)
    {
        // TODO: handle domain errors better (e.g., throw specific exceptions or use result objects)
        if (string.IsNullOrWhiteSpace(newPassword)) throw new ArgumentException("New password is required", nameof(newPassword));

        PasswordHash = hasher.Hash(newPassword);

        AddDomainEvent(new UserPasswordChanged(Id, DateTime.UtcNow));
    }

    public void ConfirmEmail()
    {
        if (EmailConfirmed)
            return;

        EmailConfirmed = true;

        AddDomainEvent(new UserEmailConfirmed(Id));
    }

    public void UpdateDisplayName(string name) => DisplayName = name;
}
