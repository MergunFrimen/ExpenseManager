using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<Transaction?> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Transaction?> GetByCategoryIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
}