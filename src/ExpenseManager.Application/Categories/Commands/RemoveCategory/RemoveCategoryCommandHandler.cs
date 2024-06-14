using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;

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
            value => new CategoryResult(value),
            ErrorOr<CategoryResult>.From
        );
    }
}