using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.TransactionAggregate.ValueObjects;

public sealed class Category : ValueObject
{
    public string Name { get; private set; }
    
    public Category(string name)
    {
        Name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}