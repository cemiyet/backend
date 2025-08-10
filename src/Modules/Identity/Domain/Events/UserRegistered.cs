using Cemiyet.Modules.Identity.Domain.ValueObjects;
using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.Events;

public sealed record UserRegistered(Guid UserId, Email Email) : DomainEvent;
