using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = Transaction.Create(
            request.UserId,
            request.Type,
            request.Category,
            request.Description,
            request.Price,
            request.Date
        );

        await transactionRepository.AddAsync(transaction, cancellationToken);

        return new TransactionResult(transaction);
    }
}