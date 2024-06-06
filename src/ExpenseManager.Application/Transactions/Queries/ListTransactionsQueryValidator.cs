using FluentValidation;

namespace ExpenseManager.Application.Transactions.Queries;

public class ListTransactionsQueryValidator : AbstractValidator<ListTransactionsQuery>
{
    public ListTransactionsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}