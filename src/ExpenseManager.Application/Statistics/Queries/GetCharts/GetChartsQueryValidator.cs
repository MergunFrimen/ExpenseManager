using FluentValidation;

namespace ExpenseManager.Application.Statistics.Queries.GetCharts;

public class GetChartsQueryValidator : AbstractValidator<GetChartsQuery>
{
    public GetChartsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}