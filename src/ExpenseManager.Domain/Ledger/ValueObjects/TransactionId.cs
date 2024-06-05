using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Ledger.ValueObjects;

public sealed class TransactionId : AggregateRootId<Guid>
{
    private TransactionId(Guid value)
    {
        Value = value;
    }

    public override Guid Value { get; protected set; }

    public static TransactionId CreateUnique()
    {
        return new TransactionId(Guid.NewGuid());
    }

    public static TransactionId Create(Guid value)
    {
        return new TransactionId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(TransactionId data)
    {
        return data.Value;
    }
}