using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users.Events;

namespace ExpenseManager.Domain.Users.Entities;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id,
        Guid userId,
        string type,
        string category,
        string description,
        decimal price,
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

    private Transaction()
    {
    }

    public Guid UserId { get; private set; }
    public string Type { get; private set; } = null!;
    public string Category { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public DateTime Date { get; private set; }

    public static Transaction Create(
        Guid userId,
        string type,
        string category,
        string description,
        decimal price,
        DateTime date
    )
    {
        var transaction = new Transaction(
            Guid.NewGuid(),
            userId,
            type,
            category,
            description,
            price,
            date
        );

        transaction.AddDomainEvent(new TransactionCreatedEvent(transaction));

        return transaction;
    }
}