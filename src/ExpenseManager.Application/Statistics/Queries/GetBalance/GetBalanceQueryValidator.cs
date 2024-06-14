using ExpenseManager.Application.Transactions.Queries.GetTransaction;
using FluentValidation;

namespace ExpenseManager.Application.Statistics.Queries.GetBalance;

public class GetBalanceQueryValidator : AbstractValidator<GetBalanceQuery>
{
    public GetBalanceQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}