using FluentValidation;

namespace ExpenseManager.Application.Statistics.Queries.GetPriceRange;

public class GetPriceRangeQueryValidator : AbstractValidator<GetPriceRangeQuery>
{
    public GetPriceRangeQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}