using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);

        if (category is null)
            return Errors.Category.CategoryNotFound;

        var transaction = Transaction.Create(
            null,
            command.UserId,
            command.Type,
            command.Description,
            command.Amount,
            command.Date,
            command.CategoryId
        );

        await transactionRepository.AddAsync(transaction, cancellationToken);

        return new TransactionResult(transaction, category.Name);
    }
}