using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        // Check if the category exists
        if (await categoryRepository.ExistsAsync(
                category => category.Id == command.Id && category.UserId == command.UserId,
                cancellationToken))
            return Errors.Category.NotFound;

        
        // Check if the category is a duplicate
        if (await categoryRepository.ExistsAsync(
                category => category.Id != command.Id && category.UserId == command.UserId && category.Name == command.Name,
                cancellationToken))
            return Errors.Category.NotFound;
        
        var category = Category.Create(command.Id, command.UserId, command.Name);
        var updatedCategory = await categoryRepository.UpdateAsync(category, cancellationToken);

        return updatedCategory.Match(
            value => new CategoryResult(value),
            ErrorOr<CategoryResult>.From
        );
    }
}