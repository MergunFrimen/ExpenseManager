using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Domain.Categories;

public sealed class Category : Entity
{
    public const int NameMaxLength = 50;

    private Category(
        Guid id,
        Guid userId,
        string name,
        List<Transaction> transactions
    ) : base(id)
    {
        UserId = userId;
        Name = name;
        Transactions = transactions;
    }

    private Category()
    {
    }

    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public static Category Create(
        Guid? id,
        Guid userId,
        string name,
        List<Transaction>? transactions = null
    )
    {
        var transaction = new Category(
            id ?? Guid.NewGuid(),
            userId,
            name,
            transactions ?? []
        );

        return transaction;
    }
}