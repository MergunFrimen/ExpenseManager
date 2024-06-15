using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;

namespace ExpenseManager.Application.Categories.Commands.RemoveCategories;

public class RemoveCategoriesCommandHandler(ICategoryRepository categoryRepository, IUserRepository userRepository)
    : ICommandHandler<RemoveCategoriesCommand, List<CategoryResult>>
{
    public async Task<ErrorOr<List<CategoryResult>>> Handle(RemoveCategoriesCommand command, CancellationToken cancellationToken)
    {
        // Get the user
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;

        // Get the categories
        var categories = await categoryRepository.FindAsync(
            category => command.CategoryIds.Contains(category.Id) && category.User.Id == user.Value.Id,
            cancellationToken
        );
        if (categories.IsError)
            return categories.Errors;
        
        var removedCategories = await categoryRepository.RemoveRangeAsync(categories.Value, cancellationToken);
        if (removedCategories.IsError)
            return removedCategories.Errors;
        
        return removedCategories.Value.Select(category => new CategoryResult(category)).ToList();
    }
}

