using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Commands.RemoveCategory;

public sealed record RemoveCategoryCommand(
    Guid Id,
    Guid UserId
) : ICommand<CategoryResult>;