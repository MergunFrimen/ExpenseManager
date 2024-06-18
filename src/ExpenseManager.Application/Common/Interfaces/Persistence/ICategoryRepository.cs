using ExpenseManager.Domain.Categories;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository : IRepository<Category>
{
    Task<ErrorOr<Category>> UpdateAsync(Category category, CancellationToken cancellationToken);
}