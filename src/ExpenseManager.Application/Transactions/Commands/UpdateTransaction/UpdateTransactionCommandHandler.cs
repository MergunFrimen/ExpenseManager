using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(UpdateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (transaction is null)
            return Errors.Transaction.TransactionNotFound;

        var category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);
        if (category is null)
            return Errors.Category.CategoryNotFound;

        var newTransaction = Transaction.Create(
            command.Id,
            command.UserId,
            command.Type,
            command.Description,
            command.Amount,
            command.Date,
            command.CategoryId
        );

        await transactionRepository.RemoveAsync(command.Id, cancellationToken);
        await transactionRepository.AddAsync(newTransaction, cancellationToken);

        return new TransactionResult(newTransaction, category.Name);
    }
}