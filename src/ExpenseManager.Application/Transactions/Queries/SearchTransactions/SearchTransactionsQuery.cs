using System.Transactions;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries.SearchTransactions;

public sealed record SearchTransactionsQuery(
    Guid UserId,
    string Description
) : IQuery<List<TransactionResult>>;