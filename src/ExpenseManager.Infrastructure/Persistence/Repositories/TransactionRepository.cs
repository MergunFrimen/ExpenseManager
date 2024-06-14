using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TransactionRepository(ExpenseManagerDbContext dbContext) : ITransactionRepository
{
    public async Task<bool> ExistsAsync(Expression<Func<Transaction, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbContext.Transactions.AnyAsync(predicate, cancellationToken);
    }

    public async Task<ErrorOr<List<Transaction>>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
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

    public async Task<ErrorOr<List<Transaction>>> FindAsync(Expression<Func<Transaction, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Transactions.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Transaction>> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var newTransaction = await dbContext.Transactions.AddAsync(transaction, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return newTransaction.Entity;
    }

    public async Task<ErrorOr<Transaction>> UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        dbContext.Transactions.Update(transaction);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<ErrorOr<Transaction>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = dbContext.Transactions.FirstOrDefault(transaction => transaction.Id == id);
        if (transaction is null)
            return Errors.Transaction.NotFound;

        dbContext.Transactions.Remove(transaction);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<ErrorOr<List<Transaction>>> RemoveRangeAsync(List<Guid> id, CancellationToken cancellationToken)
    {
        var transactions = dbContext.Transactions.Where(transaction => id.Contains(transaction.Id)).ToList();

        await dbContext
            .Transactions
            .Where(transaction => id.Contains(transaction.Id))
            .ExecuteDeleteAsync(cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transactions;
    }
}