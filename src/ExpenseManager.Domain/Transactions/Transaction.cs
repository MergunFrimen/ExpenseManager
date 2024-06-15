using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Domain.Transactions;

public sealed class Transaction : Entity
{
    public const int DescriptionMaxLength = 150;

    private Transaction(
        Guid id,
        string description,
        decimal amount,
        // TransactionType type,
        // DateTime? date,
        User user,
        List<Category> categories
    ) : base(id)
    {
        Description = description;
        Amount = amount;
        // Type = type;
        // Date = date;
        User = user;
        Categories = categories;
    }

    private Transaction()
    {
    }

    public decimal Amount { get; private set; }

    public string Description { get; private set; } = null!;

    // public TransactionType Type { get; private set; }
    // public DateTime? Date { get; private set; }
    public User User { get; private set; }
    public List<Category> Categories { get; private set; } = [];

    public static Transaction Create(
        Guid? id,
        string description,
        decimal amount,
        // TransactionType type,
        // DateTime? date,
        User user,
        List<Category>? category = null
    )
    {
        var transaction = new Transaction(
            id ?? Guid.NewGuid(),
            description,
            amount,
            // type,
            // date,
            user,
            category ?? []
        );

        return transaction;
    }
}