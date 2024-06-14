using ExpenseManager.Domain.Categories;
using ErrorOr;
using ExpenseManager.Application.Categories.Queries.SearchCategories;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<List<Category>>> SearchAsynch(SearchCategoriesQuery query, CancellationToken cancellationToken);
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken);
    Task<ErrorOr<Category>> RemoveAsync(Guid id, CancellationToken cancellationToken);
}