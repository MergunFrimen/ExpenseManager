using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Ledger.ValueObjects;

namespace ExpenseManager.Domain.Ledger.Entities;

public sealed class Transaction : Entity
{
    private Transaction(
        Guid id,
        Guid ledgerId,
        TransactionType type,
        string category,
        string description,
        decimal price,
        DateTime date
    ) : base(id)
    {
        LedgerId = ledgerId;
        Type = type;
        Category = category;
        Description = description;
        Price = price;
        Date = date;
    }

    private Transaction()
    {
    }

    public object LedgerId { get; set; } = null!;
    public TransactionType Type { get; private set; }
    public string Category { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public DateTime Date { get; private set; }

    public static Transaction Create(
        Guid ledgerId,
        TransactionType type,
        string category,
        string description,
        decimal price,
        DateTime date
    )
    {
        return new Transaction(
            Guid.NewGuid(),
            ledgerId,
            type,
            category,
            description,
            price,
            date
        );
    }
}