using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Queries.FindCategories;

public sealed record FindCategoriesQuery(
    Guid UserId,
    string Name
) : IQuery<List<CategoryResult>>;