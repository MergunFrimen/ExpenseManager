namespace ExpenseManager.Presentation.Contracts.Transactions;

public record TransactionResponse(
    Guid Id,
    Guid UserId,
    string Type,
    string Category,
    string Description,
    decimal Price,
    DateTime Date
);