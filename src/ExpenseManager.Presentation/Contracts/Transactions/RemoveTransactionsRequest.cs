namespace ExpenseManager.Presentation.Contracts.Transactions;

public sealed record RemoveTransactionsRequest(
    List<string> TransactionIds
);