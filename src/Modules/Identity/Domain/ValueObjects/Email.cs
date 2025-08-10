using Cemiyet.SharedKernel.Domain;

namespace Cemiyet.Modules.Identity.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be empty.", nameof(address));

        // TODO: add proper email validation logic
        Address = address.ToLowerInvariant();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}