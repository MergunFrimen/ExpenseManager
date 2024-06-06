using ErrorOr;

namespace ExpenseManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Transaction
    {
        public static Error TransactionNotFound => Error.NotFound(
            "Transaction.TransactionNotFound",
            "Transaction not found.");
    }
}