using ExpenseManager.Domain.Categories;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken);
    Task<Category?> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
}