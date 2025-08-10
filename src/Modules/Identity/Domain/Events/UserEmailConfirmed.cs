using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.Events;

public sealed record UserEmailConfirmed(Guid UserId) : DomainEvent;
