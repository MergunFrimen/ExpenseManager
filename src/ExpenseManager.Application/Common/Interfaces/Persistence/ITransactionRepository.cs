using ExpenseManager.Domain.Transactions;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<ErrorOr<Transaction>> UpdateAsync(Transaction transaction, CancellationToken cancellationToken);
}