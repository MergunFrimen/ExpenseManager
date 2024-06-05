using ExpenseManager.Domain.Ledger.Entities;

namespace ExpenseManager.Application.Transactions.Common;

public record TransactionResult(
    Transaction Transaction
);