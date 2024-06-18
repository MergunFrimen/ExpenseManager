using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUserRepository userRepository
)
    : ICommandHandler<UpdateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        // Get the category
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category.IsError)
            return category.Errors;

        // Get the user
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;

        // Check if the category with the same name already exists
        var isDuplicate = await categoryRepository.ExistsAsync(
            c => c.Id != command.Id && c.User.Id == user.Value.Id && c.Name == command.Name,
            cancellationToken);
        if (isDuplicate.IsError)
            return isDuplicate.Errors;
        if (isDuplicate.Value)
            return Errors.Category.Duplicate;

        // Update the transaction
        var newCategory = Category.Create(
            command.Id,
            command.Name,
            user.Value
        );

        // Update the category
        var updatedCategory = await categoryRepository.UpdateAsync(newCategory, cancellationToken);

        return updatedCategory.Match(
            value => new CategoryResult(value),
            ErrorOr<CategoryResult>.From
        );
    }
}