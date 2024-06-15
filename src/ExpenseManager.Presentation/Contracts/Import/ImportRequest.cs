namespace ExpenseManager.Presentation.Contracts.Import;

public sealed record ImportRequest(
    List<TransactionDto> Transactions,
    List<CategoryDto> Categories
);

public sealed record TransactionDto(
    string Id,
    string Description,
    decimal Amount,
    string Type,
    string? Date,
    List<CategoryDto> Categories
);

public sealed record CategoryDto(
    string Id,
    string Name
);