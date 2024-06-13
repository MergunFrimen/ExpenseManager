using ExpenseManager.Domain.Categories;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> RemoveAsync(Guid id, CancellationToken cancellationToken);
}