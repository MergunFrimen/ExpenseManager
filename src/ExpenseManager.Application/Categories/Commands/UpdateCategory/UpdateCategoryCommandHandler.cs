using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Errors.Category.CategoryNotFound;
        
        var categories = await categoryRepository.GetAllAsync(command.UserId, cancellationToken);
        
        if (categories.Any(c => c.Name == command.Name))
            return Errors.Category.CategoryAlreadyExists;
        
        var updatedCategory = Category.Create(
            command.Id,
            command.UserId,
            command.Name
        );
        
        await categoryRepository.RemoveAsync(command.Id, cancellationToken);
        await categoryRepository.AddAsync(updatedCategory, cancellationToken);
        
        return new CategoryResult(updatedCategory);
    }
}