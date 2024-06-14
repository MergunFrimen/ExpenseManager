namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record CreateTransactionRequest(
    string Description,
    decimal Amount,
    string Type,
    List<CategoryRequest> Categories
);

public sealed record CategoryRequest(
    Guid Id,
    string Name
);