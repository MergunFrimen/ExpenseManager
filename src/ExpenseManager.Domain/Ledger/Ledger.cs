using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Ledger.Entities;
using ExpenseManager.Domain.Ledger.ValueObjects;
using ExpenseManager.Domain.Users.ValueObjects;

namespace ExpenseManager.Domain.Ledger;

public sealed class Ledger : AggregateRoot<LedgerId, Guid>
{
    private readonly List<Category> _categories;
    private readonly List<Transaction> _transactions;

    private Ledger(
        LedgerId id,
        UserId userId,
        string name,
        string description,
        decimal balance,
        List<Transaction> transactions,
        List<Category> categories
    ) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        Balance = balance;
        _transactions = transactions;
        _categories = categories;
    }

    public UserId UserId { get; }
    public string Name { get; }
    public string Description { get; }

    public decimal Balance { get; }
    // public DateTime CreatedDateTime { get; }
    // public DateTime UpdatedDateTime { get; }

    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    public static Ledger Create(
        UserId userId,
        string name,
        string description,
        decimal? balance,
        List<Transaction>? transactions,
        List<Category>? categories
    )
    {
        return new Ledger(
            LedgerId.CreateUnique(),
            userId,
            name,
            description,
            balance ?? 0,
            transactions ?? [],
            categories ?? []
        );
    }
}