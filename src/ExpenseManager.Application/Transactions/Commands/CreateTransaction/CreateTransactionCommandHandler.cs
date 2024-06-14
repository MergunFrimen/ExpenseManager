using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
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
        var transaction = Transaction.Create(
            null,
            command.UserId,
            command.Type,
            command.Description,
            command.Amount,
            command.Date,
            []
        );

        await transactionRepository.AddAsync(transaction, cancellationToken);

        return new TransactionResult(transaction);
    }
}