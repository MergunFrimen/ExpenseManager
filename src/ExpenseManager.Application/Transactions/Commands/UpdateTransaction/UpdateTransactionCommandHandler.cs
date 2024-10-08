using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository
)
    : ICommandHandler<UpdateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(UpdateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        // Get the transaction
        var transaction = await transactionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (transaction.IsError)
            return transaction.Errors;

        // Get the user
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;

        // Get the categories
        var categories = await categoryRepository.FindAsync(
            category => command.CategoryIds.Contains(category.Id) && category.User.Id == user.Value.Id,
            cancellationToken
        );
        if (categories.IsError)
            return categories.Errors;

        // Update the transaction
        var newTransaction = Transaction.Create(
            command.Id,
            command.Description,
            command.Amount,
            command.Type,
            user.Value,
            command.Date,
            categories.Value
        );

        var updatedTransaction = await transactionRepository.UpdateAsync(newTransaction, cancellationToken);

        return updatedTransaction.Match(
            value => new TransactionResult(value),
            ErrorOr<TransactionResult>.From
        );
    }
}