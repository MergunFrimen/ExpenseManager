namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record SearchTransactionsRequest(
    // PaginationRequest PaginationRequest,
    FilterRequest Filters
    // SortingRequest SortingRequest
);

// public sealed record PaginationRequest(
//     int PageNumber,
//     int PageSize
// );
//
public sealed record FilterRequest(
    string? Description,
    Guid[]? CategoryIds
    // DateRangeFilterRequest? DateRangeFilterRequest,
    // PriceRangeFilterRequest? PriceRangeFilterRequest
);
//
// public sealed record DateRangeFilterRequest(
//     string? StartDate,
//     string? EndDate
// );
//
// public sealed record PriceRangeFilterRequest(
//     decimal? MinPrice,
//     decimal? MaxPrice
// );
//
// public sealed record SortingRequest(
//     string? SortBy,
//     string? SortDirection
// );