using ErrorOr;

namespace ExpenseManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            "User.DuplicateEmail",
            "Email is already in use.");

        public static Error NotFound => Error.NotFound(
            "User.NotFound",
            "User not found.");
    }
}