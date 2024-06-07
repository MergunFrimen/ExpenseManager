namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record TransactionResponse(
    string Id,
    string Type,
    string CategoryId,
    string Category,
    string Description,
    decimal Amount,
    string Date
);