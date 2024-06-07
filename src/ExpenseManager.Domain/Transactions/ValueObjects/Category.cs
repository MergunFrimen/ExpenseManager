using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Transactions.ValueObjects;

public sealed class Category : ValueObject
{
    public Category(string name)
    {
        Name = name;
    }

    private Category()
    {
    }

    public string Name { get; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}