using FluentValidation;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransactions;

public class RemoveTransactionsCommandValidator : AbstractValidator<RemoveTransactionsCommand>
{
    public RemoveTransactionsCommandValidator()
    {
        RuleFor(x => x.TransactionIds).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}