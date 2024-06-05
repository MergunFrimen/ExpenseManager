using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.ValueObjects;
using ExpenseManager.Domain.Transactions;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = Transaction.Create(
            Guid.NewGuid(),
            TransactionType.Expense,
            new Category("Something"),
            request.Description,
            new Price(100),
            DateTime.Now
        );

        transactionRepository.AddTransaction(transaction);

        return Task.FromResult<ErrorOr<TransactionResult>>(new TransactionResult(transaction));
    }
}