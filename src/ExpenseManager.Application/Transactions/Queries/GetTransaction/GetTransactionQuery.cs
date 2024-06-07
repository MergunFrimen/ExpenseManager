using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries.GetTransaction;

public sealed record GetTransactionQuery(Guid Id, Guid UserId) : IQuery<TransactionResult>;