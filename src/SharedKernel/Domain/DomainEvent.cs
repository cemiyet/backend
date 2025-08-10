namespace Cemiyet.SharedKernel.Domain;

/// <summary>
/// Base class for domain events, with timestamp and versioning.
/// </summary>
public abstract record DomainEvent
{
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    public int Version { get; protected set; } = 1;
}
