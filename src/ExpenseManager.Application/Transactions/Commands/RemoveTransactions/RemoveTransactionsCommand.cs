using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransactions;

public sealed record RemoveTransactionsCommand(
    Guid UserId,
    List<Guid> TransactionIds
) : ICommand<List<TransactionResult>>;