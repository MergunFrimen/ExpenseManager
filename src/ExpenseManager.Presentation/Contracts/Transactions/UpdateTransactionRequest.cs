namespace ExpenseManager.Presentation.Contracts.Transactions;

public record UpdateTransactionRequest(
    string TransactionId,
    string Type,
    string Category,
    string Description,
    decimal Price,
    string Date
);