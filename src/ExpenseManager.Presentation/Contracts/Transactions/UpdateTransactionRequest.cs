namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record UpdateTransactionRequest(
    string Description,
    decimal Amount,
    string Type,
    string Date,
    string[] CategoryIds
);