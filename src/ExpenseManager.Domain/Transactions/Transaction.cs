using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Common.ValueObjects;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Domain.Transactions;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id,
        Guid userId,
        TransactionType type,
        Category category,
        string description,
        Price price,
        DateTime date
    ) : base(id)
    {
        UserId = userId;
        Type = type;
        Category = category;
        Description = description;
        Price = price;
        Date = date;
    }

    public Guid UserId { get; private set; }
    public TransactionType Type { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public DateTime Date { get; private set; }

    public static Transaction Create(
        Guid userId,
        TransactionType type,
        Category category,
        string description,
        Price price,
        DateTime date
    )
    {
        return new Transaction(
            Guid.NewGuid(),
            userId,
            type,
            category,
            description,
            price,
            date
        );
    }
}