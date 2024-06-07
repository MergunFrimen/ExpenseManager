using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransaction;

public sealed record RemoveTransactionCommand(
    Guid UserId,
    Guid Id
) : ICommand<TransactionResult>;