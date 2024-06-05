using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Ledger.Entities;

namespace ExpenseManager.Domain.Ledger;

public sealed class Ledger : Entity
{
    private readonly List<Transaction> _transactions = null!;

    private Ledger(
        Guid id,
        Guid userId,
        string name,
        List<Transaction> transactions
    ) : base(id)
    {
        UserId = userId;
        Name = name;
        _transactions = transactions;
    }

    private Ledger()
    {
    }

    public Guid UserId { get; }
    public string Name { get; } = null!;

    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

    public static Ledger Create(
        Guid userId,
        string name,
        List<Transaction>? transactions
    )
    {
        return new Ledger(
            Guid.NewGuid(),
            userId,
            name,
            transactions ?? []
        );
    }
}