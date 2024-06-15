using System.Transactions;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries.SearchTransactions;

public sealed record SearchTransactionsQuery(
    Guid UserId,
    
    FilterRequest Filters
) : IQuery<List<TransactionResult>>;

public sealed record FilterRequest(
    string? Description,
    Guid[]? CategoryIds
);
