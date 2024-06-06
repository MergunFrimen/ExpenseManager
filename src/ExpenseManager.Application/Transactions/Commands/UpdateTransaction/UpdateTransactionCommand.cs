using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public sealed record UpdateTransactionCommand(
    Guid TransactionId,
    Guid UserId,
    string Type,
    string Category,
    string Description,
    decimal Price,
    DateTime Date
) : ICommand<TransactionResult>;