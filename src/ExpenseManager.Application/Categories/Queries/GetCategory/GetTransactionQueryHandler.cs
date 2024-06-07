using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryQuery, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category is null)
            return Errors.Category.CategoryNotFound;

        return new CategoryResult(category);
    }
}