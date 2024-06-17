using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;

namespace ExpenseManager.Application.Categories.Queries.SearchCategories;

public class SearchCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<SearchCategoriesQuery, List<CategoryResult>>
{
    public async Task<ErrorOr<List<CategoryResult>>> Handle(SearchCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await categoryRepository.FindAsync(
            category => category.User.Id == query.UserId, cancellationToken);
        if (result.IsError)
            return result.Errors;
        
        if (query.Filters.Name is not null)
        {
            result = result.Value.Where(
                category => category.Name.ToLower().Contains(query.Filters.Name.ToLower())
            ).ToList();
        }

        return result.Value.Select(category => new CategoryResult(category)).ToList();
    }
}