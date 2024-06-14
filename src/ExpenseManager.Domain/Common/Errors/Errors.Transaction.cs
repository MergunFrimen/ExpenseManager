using ErrorOr;

namespace ExpenseManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Transaction
    {
        public static Error NotFound => Error.NotFound(
            "Transaction.TransactionNotFound",
            "Transaction not found.");

        public static Error Unauthorized => Error.Unauthorized(
            "Transaction.Unauthorized",
            "Unauthorized.");
    }
}