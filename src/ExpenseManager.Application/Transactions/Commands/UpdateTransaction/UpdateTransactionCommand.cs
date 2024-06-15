using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public sealed record UpdateTransactionCommand(
    Guid Id,
    Guid UserId,
    
    string Description,
    decimal Amount,
    TransactionType Type,
    DateTime Date,
    List<Guid> CategoryIds
) : ICommand<TransactionResult>;