using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Application.Transactions.Common;

public record TransactionResult(
    Transaction Transaction
);