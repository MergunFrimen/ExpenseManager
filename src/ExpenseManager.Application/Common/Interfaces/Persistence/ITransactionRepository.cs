using System.Runtime.InteropServices.JavaScript;
using ErrorOr;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Transaction>> GetAllDateRangeAsync(Guid userId, DateTime? from, DateTime? to, CancellationToken cancellationToken);
    Task<ErrorOr<Transaction>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken);
    Task<ErrorOr<Transaction>> RemoveAsync(Guid id, CancellationToken cancellationToken);

}