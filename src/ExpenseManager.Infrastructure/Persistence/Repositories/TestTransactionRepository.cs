using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TestTransactionRepository(ExpenseManagerDbContext dbContext) : ITransactionRepository
{
    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var newTransaction = await dbContext.Transactions.AddAsync(transaction, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return newTransaction.Entity;
    }
    
    public async Task<Transaction?> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);

        if (transaction is null) return null;

        dbContext.Transactions.Remove(transaction);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }
    
    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);

        return transaction;
    }

    public async Task<Transaction?> GetByCategoryIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.CategoryId == id, cancellationToken: cancellationToken);

        return transaction;
    }

    public async Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync(cancellationToken);

        return transactions;
    }
}