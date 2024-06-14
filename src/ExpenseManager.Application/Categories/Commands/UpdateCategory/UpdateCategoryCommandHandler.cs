using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;

namespace ExpenseManager.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = Category.Create(command.Id, command.UserId, command.Name);
        var updatedCategory = await categoryRepository.UpdateAsync(category, cancellationToken);

        return updatedCategory.Match(
            onValue: value => new CategoryResult(value),
            onError: ErrorOr<CategoryResult>.From
        );
    }
}