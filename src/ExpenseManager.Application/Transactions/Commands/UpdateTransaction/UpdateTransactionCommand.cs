using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public sealed record UpdateTransactionCommand(
    Guid UserId,
    Guid Id,
    TransactionType Type,
    Guid CategoryId,
    string Description,
    decimal Amount,
    DateTime Date
) : ICommand<TransactionResult>;