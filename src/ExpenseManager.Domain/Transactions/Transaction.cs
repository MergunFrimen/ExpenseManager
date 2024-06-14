using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Domain.Transactions;

public sealed class Transaction : Entity
{
    public const int DescriptionMaxLength = 150;

    private Transaction(
        Guid id,
        Guid userId,
        TransactionType type,
        string description,
        decimal amount,
        DateTime? date,
        List<Category> categories
    ) : base(id)
    {
        UserId = userId;
        Type = type;
        Description = description;
        Amount = amount;
        Date = date;
        Categories = categories;
    }

    private Transaction()
    {
    }

    public Guid UserId { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = null!;
    public DateTime? Date { get; private set; }
    public List<Category> Categories { get; private set; }

    public static Transaction Create(
        Guid? id,
        Guid userId,
        TransactionType type,
        string description,
        decimal amount,
        DateTime? date,
        List<Category>? categoryIds
    )
    {
        var transaction = new Transaction(
            id ?? Guid.NewGuid(),
            userId,
            type,
            description,
            amount,
            date,
            categoryIds ?? []
        );

        return transaction;
    }
}