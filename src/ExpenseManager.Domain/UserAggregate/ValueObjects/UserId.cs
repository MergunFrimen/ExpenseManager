using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public override Guid Value { get; protected set; }

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(UserId data)
    {
        return data.Value;
    }
}