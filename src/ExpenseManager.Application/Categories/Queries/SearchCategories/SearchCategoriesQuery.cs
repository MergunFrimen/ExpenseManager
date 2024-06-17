using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Queries.SearchCategories;

public sealed record SearchCategoriesQuery(
    Guid UserId,
    FilterRequest Filters
) : IQuery<List<CategoryResult>>;

public sealed record FilterRequest(
    string? Name
);