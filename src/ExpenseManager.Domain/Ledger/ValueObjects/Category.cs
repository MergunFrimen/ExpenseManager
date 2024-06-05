using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Ledger.ValueObjects;

public sealed class Category : ValueObject
{
    public Category(string name)
    {
        Name = name;
    }

    public string Name { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}