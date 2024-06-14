using ExpenseManager.Application.Common.Extensions;
using ExpenseManager.Domain.Transactions;
using FluentValidation;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.CategoryIds).NotEmpty();
        RuleFor(x => x.Type).TransactionType();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(Transaction.DescriptionMaxLength);
        RuleFor(x => x.Amount).MoneyAmount();
        RuleFor(x => x.Date).NotEmpty();
    }
}