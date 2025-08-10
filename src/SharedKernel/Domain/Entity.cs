namespace Cemiyet.SharedKernel.Domain;

/// <summary>
/// Base class for all entities with a generic typed ID and common equality logic.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier, which must not be null.</typeparam>
public abstract class Entity<TId> where TId : notnull
{
    public TId Id { get; protected set; } = default!;

    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent eventItem) => _domainEvents.Add(eventItem);
    public void ClearDomainEvents() => _domainEvents.Clear();

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}
