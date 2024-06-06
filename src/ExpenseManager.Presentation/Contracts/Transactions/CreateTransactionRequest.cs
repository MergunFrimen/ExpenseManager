namespace ExpenseManager.Presentation.Contracts.Transactions;

public record CreateTransactionRequest(
    string Type,
    string Category,
    string Description,
    decimal Price,
    string Date
);