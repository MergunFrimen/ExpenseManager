using ErrorOr;

namespace ExpenseManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            "User.InvalidCredentials",
            "Invalid credentials.");

        public static Error MissingNameIdentifier => Error.Validation(
            "User.MissingNameIdentifier",
            "Missing NameIdentifier claim.");
    }
}