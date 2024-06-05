using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Common.ValueObjects;

namespace ExpenseManager.Domain.Ledger;

public sealed class Ledger : Entity
{
    private readonly List<Category> _categories;
    private readonly List<Guid> _transactionsIds;

    private Ledger(
        Guid id,
        Guid userId,
        string name,
        string description,
        decimal balance,
        List<Guid> transactionsIds,
        List<Category> categories
    ) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        Balance = balance;
        _transactionsIds = transactionsIds;
        _categories = categories;
    }

    public Guid UserId { get; }
    public string Name { get; }
    public string Description { get; }

    public decimal Balance { get; }
    // public DateTime CreatedDateTime { get; }
    // public DateTime UpdatedDateTime { get; }

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();
    public IReadOnlyList<Guid> TransactionIds => _transactionsIds.AsReadOnly();

    public static Ledger Create(
        Guid userId,
        string name,
        string description,
        decimal? balance,
        List<Guid>? transactionIds,
        List<Category>? categories
    )
    {
        return new Ledger(
            Guid.NewGuid(),
            userId,
            name,
            description,
            balance ?? 0,
            transactionIds ?? [],
            categories ?? []
        );
    }
}