namespace ExpenseManager.Presentation.Contracts.DataTransfer;

public sealed record ImportRequest(
    List<TransactionDto> transactions,
    List<CategoryDto> categories
);

public sealed record TransactionDto(
    string id,
    string userId,
    string type,
    string description,
    decimal amount,
    string? date,
    string[]? categoryIds
);

public sealed record CategoryDto(
    string id,
    string userId,
    string name
);
