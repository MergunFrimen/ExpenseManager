using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Categories.Commands.RemoveCategories;

public sealed record RemoveCategoriesCommand(
    Guid UserId,
    
    List<Guid> CategoryIds
) : ICommand<List<CategoryResult>>;