using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Ledger.Entities;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TransactionRepository(ExpenseManagerDbContext dbContext) : ITransactionRepository
{
    public void Add(Transaction transaction)
    {
        dbContext.Add(transaction);
        dbContext.SaveChanges();
    }
}