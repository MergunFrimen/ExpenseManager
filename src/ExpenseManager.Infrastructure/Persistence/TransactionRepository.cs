using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Infrastructure.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactions = [];

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }
}