using ErrorOr;

namespace ExpenseManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NotFound => Error.NotFound(
            "Category.CategoryNotFound",
            "Category not found.");

        public static Error Duplicate => Error.Conflict(
            "Category.Duplicate",
            "Category with name already exists.");

        // TODO: remove this
        public static Error CategoryUsedInTransactions => Error.Conflict(
            "Category.CategoryUsedInTransactions",
            "Category is used in transactions.");
    }
}