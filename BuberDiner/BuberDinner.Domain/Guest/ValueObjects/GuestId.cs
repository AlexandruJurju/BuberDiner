using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Guest.ValueObjects;

public sealed class GuestId : ValueObject
{
    private GuestId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static GuestId CreateUnique()
    {
        return new GuestId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}