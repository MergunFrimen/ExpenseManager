using System.Linq.Expressions;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface IRepository<T>
{
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<ErrorOr<List<T>>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ErrorOr<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<List<T>>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<ErrorOr<T>> AddAsync(T entity, CancellationToken cancellationToken);
    Task<ErrorOr<T>> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<ErrorOr<T>> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<List<T>>> RemoveRangeAsync(List<Guid> id, CancellationToken cancellationToken);
}