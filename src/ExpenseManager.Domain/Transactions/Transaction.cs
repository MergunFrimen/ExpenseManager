using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Transactions.Events;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Domain.Transactions;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id,
        Guid userId,
        TransactionType type,
        string description,
        decimal amount,
        DateTime date,
        Guid? categoryId
    ) : base(id)
    {
        UserId = userId;
        Type = type;
        Description = description;
        Amount = amount;
        Date = date;
        CategoryId = categoryId;
    }

    private Transaction()
    {
    }

    public Guid UserId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public TransactionType Type { get; private set; }
    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }

    public static Transaction Create(
        Guid userId,
        TransactionType type,
        string description,
        decimal amount,
        DateTime date,
        Guid? categoryId
    )
    {
        var transaction = new Transaction(
            Guid.NewGuid(),
            userId,
            type,
            description,
            amount,
            date,
            categoryId
        );

        transaction.AddDomainEvent(new TransactionCreatedEvent(transaction));

        return transaction;
    }
}