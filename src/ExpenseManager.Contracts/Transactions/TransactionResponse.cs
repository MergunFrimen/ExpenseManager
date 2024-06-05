namespace ExpenseManager.Contracts.Transactions;

public record TransactionResponse(
    Guid Id,
    Guid LedgerId,
    string Type,
    string Category,
    string Description,
    decimal Price,
    DateTime Date
);