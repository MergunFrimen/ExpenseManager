using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;

namespace ExpenseManager.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler(
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryQuery, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(query.Id, cancellationToken);

        return category.Match(
            value => new CategoryResult(value),
            ErrorOr<CategoryResult>.From
        );
    }
}