using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<UpdateTransactionCommand, TransactionResult>
{
    public Task<ErrorOr<TransactionResult>> Handle(UpdateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = transactionRepository.GetById(command.TransactionId);

        if (transaction is null)
            return Task.FromResult<ErrorOr<TransactionResult>>(Errors.Transaction.TransactionNotFound);

        transactionRepository.Remove(command.TransactionId);
        var newTransaction = transactionRepository.Add(Transaction.Create(
            transaction.UserId,
            command.Type,
            command.Category,
            command.Description,
            command.Price,
            command.Date
        ));

        return Task.FromResult<ErrorOr<TransactionResult>>(new TransactionResult(newTransaction));
    }
}