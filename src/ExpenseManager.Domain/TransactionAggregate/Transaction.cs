using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.TransactionAggregate.ValueObjects;
using ExpenseManager.Domain.UserAggregate.ValueObjects;

namespace ExpenseManager.Domain.TransactionAggregate;

public sealed class Transaction : AggregateRoot<TransactionId, Guid>
{
    public Transaction(
        TransactionId id,
        UserId userId,
        TransactionType type,
        Category category,
        string description,
        Price price,
        DateTime date
    ) : base(id)
    {
        Id = id;
        UserId = userId;
        Type = type;
        Category = category;
        Description = description;
        Price = price;
        Date = date;
    }

    public UserId UserId { get; private set; }
    public TransactionType Type { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public DateTime Date { get; private set; }

    public static Transaction Create(
        UserId userId,
        TransactionType type,
        Category category,
        string description,
        Price price,
        DateTime date
    )
    {
        return new Transaction(
            TransactionId.CreateUnique(),
            userId,
            type,
            category,
            description,
            price,
            date
        );
    }
}