namespace ExpenseManager.Presentation.Contracts.Categories;

public sealed record SearchCategoriesRequest(
    PaginationRequest PaginationRequest,
    FilterRequest FilterRequest,
    SortingRequest SortingRequest
);

public sealed record PaginationRequest(
    int PageNumber,
    int PageSize
);

public sealed record FilterRequest(
    string? DescriptionFilter,
    string[]? CategoryFilter,
    DateRangeFilterRequest? DateRangeFilterRequest,
    PriceRangeFilterRequest? PriceRangeFilterRequest
);

public sealed record DateRangeFilterRequest(
    string? StartDate,
    string? EndDate
);

public sealed record PriceRangeFilterRequest(
    decimal? MinPrice,
    decimal? MaxPrice
);

public sealed record SortingRequest(
    string? SortBy,
    string? SortDirection
);