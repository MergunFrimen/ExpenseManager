using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Queries.GetCategory;

public sealed record GetCategoryQuery(Guid Id, Guid UserId) : IQuery<CategoryResult>;