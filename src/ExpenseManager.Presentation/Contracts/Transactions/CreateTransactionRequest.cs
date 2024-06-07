namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record CreateTransactionRequest(
    string Type,
    string? CategoryId,
    string Description,
    decimal Amount,
    string Date
);