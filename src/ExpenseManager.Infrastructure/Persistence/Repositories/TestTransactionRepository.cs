using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TestTransactionRepository : ITransactionRepository
{
    private static readonly List<Transaction> Transactions = [];

    public Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        Transactions.Add(transaction);

        return Task.FromResult(transaction);
    }
    
    public Task<Transaction?> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == id);

        if (transaction is null) return Task.FromResult<Transaction?>(null);

        Transactions.Remove(transaction);

        return Task.FromResult<Transaction?>(transaction);
    }
    
    public Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == id);

        return Task.FromResult(transaction);
    }

    public Task<Transaction?> GetByCategoryIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var transaction = Transactions.FirstOrDefault(t => t.CategoryId == id);

        return Task.FromResult(transaction);
    }

    public Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Task.FromResult(Transactions);
    }
}