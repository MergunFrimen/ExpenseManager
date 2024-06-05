using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
    string Type,
    string Category,
    string Description,
    decimal Price,
    string Date
) : ICommand<TransactionResult>;