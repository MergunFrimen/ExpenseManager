namespace ExpenseManager.Presentation.Contracts.Categories;

public sealed record SearchCategoriesRequest(
    FilterRequest Filters
);

public sealed record FilterRequest(
    string? Name
);
