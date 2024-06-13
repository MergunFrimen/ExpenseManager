using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Categories.Commands.RemoveCategory;

public class RemoveCategoryCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<RemoveCategoryCommand, CategoryResult>
{
    public async Task<ErrorOr<CategoryResult>> Handle(RemoveCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Errors.Category.NotFound;

        if (category.UserId != command.UserId)
            return Error.Unauthorized();

        var transactions = await transactionRepository.GetAllAsync(command.UserId, cancellationToken);
        
        await categoryRepository.RemoveAsync(command.Id, cancellationToken);

        return new CategoryResult(category);
    }
}