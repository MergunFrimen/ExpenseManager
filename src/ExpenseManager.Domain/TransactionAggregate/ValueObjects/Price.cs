using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.TransactionAggregate.ValueObjects;

public sealed class Price : ValueObject
{
    public Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}