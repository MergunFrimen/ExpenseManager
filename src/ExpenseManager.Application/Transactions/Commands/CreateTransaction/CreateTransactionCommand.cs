using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid LedgerId,
    string Type,
    string Category,
    string Description,
    decimal Price,
    DateTime Date
) : ICommand<TransactionResult>;