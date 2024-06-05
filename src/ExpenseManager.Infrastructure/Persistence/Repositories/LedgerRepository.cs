using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Ledger.Entities;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class LedgerRepository() : ITransactionRepository
{
    private static readonly List<Transaction> _transactions = [];

    public void Add(Transaction transaction)
    {
        _transactions.Add(transaction);
    }
}