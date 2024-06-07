using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(
    Guid UserId,
    string Name
) : ICommand<CategoryResult>;