using ErrorOr;

namespace ExpenseManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error CategoryNotFound => Error.NotFound(
            "Category.CategoryNotFound",
            "Category not found.");

        public static Error CategoryAlreadyExists => Error.Conflict(
            "Category.CategoryAlreadyExists",
            "Category already exists.");

        public static Error CategoryUsedInTransactions => Error.Conflict(
            "Category.CategoryUsedInTransactions",
            "Category is used in transactions.");
    }
}