using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Ledger.Entities;
using ExpenseManager.Domain.Ledger.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = Transaction.Create(
            request.LedgerId,
            TransactionType.Expense,
            request.Category,
            request.Description,
            request.Price,
            request.Date
        );

        transactionRepository.Add(transaction);

        return Task.FromResult<ErrorOr<TransactionResult>>(new TransactionResult(transaction));
    }
}