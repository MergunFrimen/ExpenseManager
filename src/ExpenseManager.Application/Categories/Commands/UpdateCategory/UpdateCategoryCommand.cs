using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid UserId,
    Guid Id,
    string Name
) : ICommand<CategoryResult>;