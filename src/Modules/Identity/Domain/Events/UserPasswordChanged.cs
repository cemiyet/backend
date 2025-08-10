using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.Events;

public sealed record UserPasswordChanged(Guid UserId, DateTime ChangedAt) : DomainEvent;
