using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public sealed record UpdateTransactionCommand(
    Guid TransactionId,
    Guid UserId,
    TransactionType Type,
    string Description,
    decimal Price,
    DateTime Date,
    Guid? CategoryId
) : ICommand<TransactionResult>;