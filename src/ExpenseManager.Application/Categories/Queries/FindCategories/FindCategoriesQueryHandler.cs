using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;

namespace ExpenseManager.Application.Categories.Queries.FindCategories;

public class FindCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<FindCategoriesQuery, List<CategoryResult>>
{
    public async Task<ErrorOr<List<CategoryResult>>> Handle(FindCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.FindAsync(
            category => category.UserId == query.UserId && category.Name.Contains(query.Name), cancellationToken);

        return categories.Match(
            value => value.Select(category => new CategoryResult(category)).ToList(),
            ErrorOr<List<CategoryResult>>.From
        );
    }
}