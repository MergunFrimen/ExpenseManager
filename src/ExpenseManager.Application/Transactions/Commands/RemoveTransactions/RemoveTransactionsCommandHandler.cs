using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransactions;

public class RemoveTransactionsCommandHandler(
    ITransactionRepository transactionRepository,
    IUserRepository userRepository)
    : ICommandHandler<RemoveTransactionsCommand, List<TransactionResult>>
{
    public async Task<ErrorOr<List<TransactionResult>>> Handle(RemoveTransactionsCommand command,
        CancellationToken cancellationToken)
    {
        // Get the user
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;

        // Get the transactions
        var transactions = await transactionRepository.FindAsync(
            transaction => command.TransactionIds.Contains(transaction.Id) && transaction.User.Id == user.Value.Id,
            cancellationToken);
        if (transactions.IsError)
            return transactions.Errors;

        var removedTransactions = await transactionRepository.RemoveRangeAsync(transactions.Value, cancellationToken);
        if (removedTransactions.IsError)
            return removedTransactions.Errors;

        return removedTransactions.Value.Select(category => new TransactionResult(category)).ToList();
    }
}