using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Queries;

public sealed record ListCategoriesQuery(
    Guid UserId
) : IQuery<List<CategoryResult>>;