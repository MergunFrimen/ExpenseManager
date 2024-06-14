using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;

namespace ExpenseManager.Application.Categories.Queries.SearchCategories;

public class SearchCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<SearchCategoriesQuery, List<CategoryResult>>
{
    public async Task<ErrorOr<List<CategoryResult>>> Handle(SearchCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.SearchAsynch(query, cancellationToken);

        return categories.Match(
            onValue: value => value.Select(category => new CategoryResult(category)).ToList(),
            onError: ErrorOr<List<CategoryResult>>.From
        );
    }
}