namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record UpdateTransactionRequest(
    string Id,
    string Type,
    string? CategoryId,
    string Description,
    decimal Amount,
    string Date
);