using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Transactions.Common;

public record TransactionResult(
    string? Category,
    Transaction Transaction
);