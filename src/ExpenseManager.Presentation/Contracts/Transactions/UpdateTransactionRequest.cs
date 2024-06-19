namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record UpdateTransactionRequest(
    string Description,
    decimal Amount,
    string Type,
    ulong? Date,
    string[] CategoryIds
);