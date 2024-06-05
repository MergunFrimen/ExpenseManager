using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    void AddTransaction(Transaction transaction);
}