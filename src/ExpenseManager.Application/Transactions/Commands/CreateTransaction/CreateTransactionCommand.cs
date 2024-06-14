using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid UserId,
    Guid[] CategoryIds,
    TransactionType Type,
    string Description,
    decimal Amount,
    DateTime Date
) : ICommand<TransactionResult>;