using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.Events;

public sealed record UserRegistered(Guid UserId, string Email) : DomainEvent;
