namespace ExpenseManager.Contracts.Transactions;

public record RemoveTransactionRequest(
    Guid UserId,
    Guid TransactionId
);