using ErrorOr;
using ExpenseManager.Domain.Categories;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ICategoryRepository : IRepository<Category>
{
    Task<ErrorOr<Category>> UpdateAsync(Category category, CancellationToken cancellationToken);
}