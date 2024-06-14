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
        var removedCategory = await categoryRepository.RemoveAsync(command.Id, cancellationToken);

        return removedCategory.Match(
            onValue: value => new CategoryResult(value),
            onError: ErrorOr<CategoryResult>.From
        );
    }
}