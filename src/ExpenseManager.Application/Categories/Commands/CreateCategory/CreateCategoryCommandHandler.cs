using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : ICommandHandler<CreateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await categoryRepository.ExistsAsync(
            category => category.UserId == command.UserId &&
                        category.Name == command.Name,
            cancellationToken);
        
        if (exists)
            return Errors.Category.Duplicate;

        var newCategory = Category.Create(null, command.UserId, command.Name);

        await categoryRepository.AddAsync(newCategory, cancellationToken);

        return new CategoryResult(newCategory);
    }
}