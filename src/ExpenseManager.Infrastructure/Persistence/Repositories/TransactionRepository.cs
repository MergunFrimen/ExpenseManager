using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TransactionRepository(ExpenseManagerDbContext dbContext) : ITransactionRepository
{
    public async Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync(cancellationToken);
        
        return transactions;
    }

    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var newTransaction = await dbContext.Transactions.AddAsync(transaction, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return newTransaction.Entity;
    }

    public async Task<ErrorOr<Transaction>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Transactions.FindAsync(id, cancellationToken) is not { } transaction)
            return Errors.Transaction.TransactionNotFound;
        
        dbContext.Transactions.Remove(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<ErrorOr<Transaction>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Transactions.FindAsync(id, cancellationToken) is not { } transaction)
            return Errors.Transaction.TransactionNotFound;
        
        return transaction;
    }
}