using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid UserId,
    
    string Description,
    decimal Amount,
    TransactionType Type,
    DateTime Date,
    Guid[] CategoryIds
) : ICommand<TransactionResult>;