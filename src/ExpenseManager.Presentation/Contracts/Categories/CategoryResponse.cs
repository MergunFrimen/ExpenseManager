namespace ExpenseManager.Presentation.Contracts.Categories;

public sealed record CategoryResponse(
    string Id,
    string Name,
    string UserId
    // List<TransactionResponse> Transactions
);

public sealed record TransactionResponse(
    string Id
);