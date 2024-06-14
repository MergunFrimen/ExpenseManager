namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record CreateTransactionRequest(
    string Description,
    decimal Amount,
    string Type,
    string[] CategoryIds,
    string Date
);