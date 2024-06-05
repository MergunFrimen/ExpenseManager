using ExpenseManager.Domain.Ledger.Entities;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    void Add(Transaction transaction);
}