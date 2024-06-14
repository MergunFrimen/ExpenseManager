using System.Linq.Expressions;
using ExpenseManager.Domain.Categories;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository: IRepository<Category>
{
    new Task<ErrorOr<List<Category>>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    new Task<ErrorOr<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    new Task<ErrorOr<List<Category>>> FindAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);

    new Task<ErrorOr<Category>> AddAsync(Category category, CancellationToken cancellationToken);
    new Task<ErrorOr<Category>> UpdateAsync(Category category, CancellationToken cancellationToken);
    new Task<ErrorOr<Category>> RemoveAsync(Guid id, CancellationToken cancellationToken);
    new Task<ErrorOr<List<Category>>> RemoveRangeAsync(List<Guid> id, CancellationToken cancellationToken);
}