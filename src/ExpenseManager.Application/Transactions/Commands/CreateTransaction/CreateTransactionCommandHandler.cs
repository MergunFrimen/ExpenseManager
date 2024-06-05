using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}