using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Ledger.ValueObjects;

public sealed class LedgerId : AggregateRootId<Guid>
{
    private LedgerId(Guid value)
    {
        Value = value;
    }

    public override Guid Value { get; protected set; }

    public static LedgerId CreateUnique()
    {
        return new LedgerId(Guid.NewGuid());
    }

    public static LedgerId Create(Guid value)
    {
        return new LedgerId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(LedgerId data)
    {
        return data.Value;
    }
}