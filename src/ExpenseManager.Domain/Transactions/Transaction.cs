using ExpenseManager.Domain.Common.Models;
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
        DateTime? date,
        List<Guid> categoryIds
    ) : base(id)
    {
        UserId = userId;
        Type = type;
        Description = description;
        Amount = amount;
        Date = date;
        CategoryIds = categoryIds;
    }

    private Transaction()
    {
    }

    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public List<Guid> CategoryIds { get; private set; }
    public Guid UserId { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime? Date { get; private set; }

    public const int DescriptionMaxLength = 150;

    public static Transaction Create(
        Guid? id,
        Guid userId,
        TransactionType type,
        string description,
        decimal amount,
        DateTime? date,
        List<Guid>? categoryIds
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