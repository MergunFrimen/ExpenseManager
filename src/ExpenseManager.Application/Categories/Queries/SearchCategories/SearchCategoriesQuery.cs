using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Queries.SearchCategories;

public sealed record SearchCategoriesQuery(
    Guid UserId,
    string Name
) : IQuery<List<CategoryResult>>;