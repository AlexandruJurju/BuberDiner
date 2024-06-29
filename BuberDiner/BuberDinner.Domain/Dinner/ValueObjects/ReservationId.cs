using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Dinner.ValueObjects;

public sealed class ReservationId : ValueObject
{
    private ReservationId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static ReservationId CreateUnique()
    {
        return new ReservationId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public ReservationId()
    {
    }
}