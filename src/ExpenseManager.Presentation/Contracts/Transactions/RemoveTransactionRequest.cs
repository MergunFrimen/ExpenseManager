namespace ExpenseManager.Presentation.Contracts.Transactions;

public record RemoveTransactionRequest(
    Guid TransactionId
);