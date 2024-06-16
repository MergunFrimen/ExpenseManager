using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions.ValueObjects;
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

    public static void MoneyAmount<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty()
            .GreaterThan(0).WithMessage("Amount must be greater than 0");
    }

    public static void CategoryName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty()
            .MaximumLength(Category.NameMaxLength)
            .Matches("[a-zA-Z0-9 ]").WithMessage("Name name must be alphanumeric.");
    }

    public static void TransactionType<T>(this IRuleBuilder<T, TransactionType> ruleBuilder)
    {
        var options = ruleBuilder
            .IsInEnum().WithMessage("Transaction type must be either Income or Expense");
    }
}