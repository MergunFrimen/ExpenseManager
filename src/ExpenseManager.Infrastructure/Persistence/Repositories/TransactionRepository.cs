using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TransactionRepository(ExpenseManagerDbContext dbContext) : ITransactionRepository
{
    public async Task<bool> ExistsAsync(Expression<Func<Transaction, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Transactions.AnyAsync(predicate, cancellationToken);
    }

    public async Task<List<Transaction>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Transactions
            .Where(transaction => transaction.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Transaction>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Transactions.FirstOrDefaultAsync(c => c.Id == id, cancellationToken) is not { } transaction)
            return Errors.Transaction.NotFound;

        return transaction;
    }

    public async Task<List<Transaction>> FindAsync(Expression<Func<Transaction, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Transactions.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var newTransaction = await dbContext.Transactions.AddAsync(transaction, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return newTransaction.Entity;
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var updatedTransaction = dbContext.Transactions.Update(transaction);

        await dbContext.SaveChangesAsync(cancellationToken);

        return updatedTransaction.Entity;
    }

    public async Task<Transaction> RemoveAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        dbContext.Transactions.Remove(transaction);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<List<Transaction>> RemoveRangeAsync(List<Transaction> transactions,
        CancellationToken cancellationToken)
    {
        dbContext.Transactions.RemoveRange(transactions);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transactions;
    }
}