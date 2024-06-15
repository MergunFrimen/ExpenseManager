using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Transactions.Queries.SearchTransactions;

public sealed record SearchTransactionsQuery(
    Guid UserId,
    FilterRequest Filters
) : IQuery<List<TransactionResult>>;

public sealed record FilterRequest(
    string? Description,
    TransactionType? TransactionType,
    Guid[]? CategoryIds,
    DateRange? DateRange,
    PriceRange? PriceRange
);

public sealed record DateRange(
    ulong? From,
    ulong? To
);

public sealed record PriceRange(
    decimal? From,
    decimal? To
);