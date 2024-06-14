namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record UpdateTransactionRequest(
    string Id,
    string Type,
    string[] CategoryIds,
    string Description,
    decimal Amount,
    string Date
);