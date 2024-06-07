namespace ExpenseManager.Presentation.Contracts.Transactions;

public record TransactionResponse(
    string Id,
    string UserId,
    string Type,
    string Description,
    decimal Price,
    string Category,
    DateTime Date
);