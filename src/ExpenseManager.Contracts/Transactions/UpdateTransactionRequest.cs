namespace ExpenseManager.Contracts.Transactions;

public record UpdateTransactionRequest(
    Guid UserId,
    string TransactionId,
    string Type,
    string Category,
    string Description,
    decimal Price,
    string Date
);