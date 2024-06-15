namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record SearchTransactionsRequest(
    FilterRequest Filters
    // PaginationRequest PaginationRequest,
    // SortingRequest SortingRequest
);

public sealed record FilterRequest(
    string? Description,
    string? TransactionType,
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

// public sealed record SortingRequest(
//     string? SortBy,
//     string? SortDirection
// );

// public sealed record PaginationRequest(
//     int PageNumber,
//     int PageSize
// );
//
