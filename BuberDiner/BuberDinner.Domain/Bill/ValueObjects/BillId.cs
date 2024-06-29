using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Bill.ValueObjects;

public sealed class BillId : ValueObject
{
    private BillId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BillId CreateUnique()
    {
        return new BillId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}