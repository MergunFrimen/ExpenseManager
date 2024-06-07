using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Transactions.Common;

public record TransactionResult(
    Transaction Transaction,
    string Category
);