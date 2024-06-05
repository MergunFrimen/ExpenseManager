using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Ledger.ValueObjects;

namespace ExpenseManager.Domain.Ledger.Entities;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id,
        TransactionType type,
        Category category,
        string description,
        Price price,
        DateTime date
    ) : base(id)
    {
        Type = type;
        Category = category;
        Description = description;
        Price = price;
        Date = date;
    }

    private Transaction()
    {
    }

    public TransactionType Type { get; private set; }
    public Category Category { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public Price Price { get; private set; } = null!;
    public DateTime Date { get; private set; }

    public static Transaction Create(
        TransactionType type,
        Category category,
        string description,
        Price price,
        DateTime date
    )
    {
        return new Transaction(
            Guid.NewGuid(),
            type,
            category,
            description,
            price,
            date
        );
    }
}