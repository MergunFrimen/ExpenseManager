using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TransactionRepository(ExpenseManagerDbContext dbContext) : ITransactionRepository
{
    private static List<Transaction> _transactions = [];
    
    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<Transaction?> RemoveAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId,
            cancellationToken);

        if (transaction is null) return transaction;

        dbContext.Transactions.Remove(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<Transaction?> UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var existingTransaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id,
            cancellationToken);

        if (existingTransaction is null) return existingTransaction;

        dbContext.Transactions.Remove(existingTransaction);
        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);

        return existingTransaction;
    }

    public async Task<Transaction?> GetByIdAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId,
            cancellationToken);

        return transaction;
    }

    public Task<Transaction?> GetByCategoryIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync(cancellationToken);

        return transactions;
    }
}