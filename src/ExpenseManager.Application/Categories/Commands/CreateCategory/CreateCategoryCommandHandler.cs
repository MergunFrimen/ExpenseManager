using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUserRepository userRepository)
    : ICommandHandler<CreateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        // Check if category with the same name already exists
        var exists = await categoryRepository.ExistsAsync(category => category.Name == command.Name, cancellationToken);
        if (exists.IsError)
            return exists.Errors;
        if (exists.Value)
            return Errors.Category.Duplicate;

        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;
        
        var category = Category.Create(null, command.Name, command.UserId);

        var createdCategory = await categoryRepository.AddAsync(category, cancellationToken);
        if (createdCategory.IsError)
            return createdCategory.Errors;

        return new CategoryResult(createdCategory.Value);
    }
}