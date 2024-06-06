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
    public async Task<ErrorOr<TransactionResult>> Handle(UpdateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetByIdAsync(command.TransactionId, cancellationToken);

        if (transaction is null)
            return Errors.Transaction.TransactionNotFound;

        await transactionRepository.RemoveAsync(command.TransactionId, cancellationToken);
        var newTransaction = await transactionRepository.AddAsync(Transaction.Create(
            transaction.UserId,
            command.Type,
            command.Category,
            command.Description,
            command.Price,
            command.Date
        ), cancellationToken);

        return new TransactionResult(newTransaction);
    }
}