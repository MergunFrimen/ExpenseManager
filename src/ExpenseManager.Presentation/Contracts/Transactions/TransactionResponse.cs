namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record TransactionResponse(
    string Id,
    string Description,
    decimal Amount,
    string Type,
    string Date,
    
    string[] CategoryIds,
    string[] CategoryNames
);