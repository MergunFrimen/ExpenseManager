using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<Transaction?> RemoveAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<Transaction?> UpdateAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<Transaction?> GetByIdAsync(Guid transactionId, CancellationToken cancellationToken);
    Task<List<Transaction>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}