using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransaction;

public class RemoveTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<RemoveTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(RemoveTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.RemoveAsync(request.TransactionId, cancellationToken);

        if (transaction is null)
            return Errors.Transaction.TransactionNotFound;

        return new TransactionResult(transaction);
    }
}