using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid UserId,
    TransactionType Type,
    string Description,
    decimal Price,
    DateTime Date,
    Guid? CategoryId
) : ICommand<TransactionResult>;