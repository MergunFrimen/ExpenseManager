namespace ExpenseManager.Contracts.Transactions;

public record TransactionResponse(
    Guid Id,
    string Type,
    string Category,
    string Description,
    decimal Price,
    DateTime Date
);