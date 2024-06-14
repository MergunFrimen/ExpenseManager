using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransaction;

public class RemoveTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<RemoveTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(RemoveTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.RemoveAsync(command.Id, cancellationToken);
        
        return transaction.Match(
            onValue: value => new TransactionResult(value),
            onError: ErrorOr<TransactionResult>.From
        );
    }
}