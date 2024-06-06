using FluentValidation;

namespace ExpenseManager.Application.Common.Extensions;

public static class RuleBuilderExtensions
{
    public static void Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
    {
        var options = ruleBuilder
            .NotEmpty()
            .MinimumLength(minimumLength).WithMessage($"Password must be at least {minimumLength} characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
    }
}