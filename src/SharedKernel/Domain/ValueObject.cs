namespace Cemiyet.SharedKernel.Domain;

/// <summary>
/// Simple base class for immutable value objects with equality by value.
/// </summary>
public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(0, (hash, obj) =>
            {
                unchecked
                {
                    return (hash * 23) + (obj?.GetHashCode() ?? 0);
                }
            });
    }
}
