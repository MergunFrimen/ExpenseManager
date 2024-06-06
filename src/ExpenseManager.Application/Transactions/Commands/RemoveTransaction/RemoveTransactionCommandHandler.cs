using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransaction;

public class RemoveTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<RemoveTransactionCommand, TransactionResult>
{
    public Task<ErrorOr<TransactionResult>> Handle(RemoveTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = transactionRepository.Remove(request.TransactionId);

        if (transaction is null)
            return Task.FromResult<ErrorOr<TransactionResult>>(Errors.Transaction.TransactionNotFound);

        return Task.FromResult<ErrorOr<TransactionResult>>(new TransactionResult(transaction));
    }
}