namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record TransactionResponse(
    string Id,
    string Description,
    decimal Amount,
    string Type,
    string[] CategoryIds,
    string Date,
    string[] CategoryNames
);