using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface IRepository<T>
{
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<List<T>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ErrorOr<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<T> RemoveAsync(T transaction, CancellationToken cancellationToken);
    Task<List<T>> RemoveRangeAsync(List<T> transactions, CancellationToken cancellationToken);
}