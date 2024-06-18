using ErrorOr;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<ErrorOr<Transaction>> UpdateAsync(Transaction transaction, CancellationToken cancellationToken);
}