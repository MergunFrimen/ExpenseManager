using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;

namespace ExpenseManager.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : ICommandHandler<CreateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var newCategory = Category.Create(null, command.UserId, command.Name);

        await categoryRepository.AddAsync(newCategory, cancellationToken);

        return new CategoryResult(newCategory);
    }
}