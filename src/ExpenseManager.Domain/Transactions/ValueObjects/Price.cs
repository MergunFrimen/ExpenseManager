using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Transactions.ValueObjects;

public sealed class Price : ValueObject
{
    public Price(decimal amount)
    {
        Amount = amount;
    }

    public decimal Amount { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}