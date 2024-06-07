using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries;

public sealed record ListTransactionsQuery(Guid UserId) : IQuery<List<TransactionResult>>;