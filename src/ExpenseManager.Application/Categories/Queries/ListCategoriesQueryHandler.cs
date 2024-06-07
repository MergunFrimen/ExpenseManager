using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;

namespace ExpenseManager.Application.Categories.Queries;

public class ListCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<ListCategoriesQuery, List<CategoryResult>>
{
    public async Task<ErrorOr<List<CategoryResult>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(request.UserId, cancellationToken);
        
        return categories.Select(c => new CategoryResult(c)).ToList();
    }
}