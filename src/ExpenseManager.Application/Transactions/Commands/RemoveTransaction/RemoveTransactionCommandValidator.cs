using FluentValidation;

namespace ExpenseManager.Application.Transactions.Commands.RemoveTransaction;

public class RemoveTransactionCommandValidator : AbstractValidator<RemoveTransactionCommand>
{
    public RemoveTransactionCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
    }
}