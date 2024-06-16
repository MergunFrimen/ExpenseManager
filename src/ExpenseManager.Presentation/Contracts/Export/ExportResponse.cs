namespace ExpenseManager.Presentation.Contracts.Export;

public sealed record ExportResponse(
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