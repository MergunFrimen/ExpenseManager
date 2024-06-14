using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetCharts;

public class GetChartsQueryHandler(
    IChartsService chartsService)
    : IQueryHandler<GetChartsQuery, ChartsResult>
{
    public async Task<ErrorOr<ChartsResult>> Handle(GetChartsQuery query, CancellationToken cancellationToken)
    {
        var from = query.From ?? DateTime.MinValue;
        var to = query.To ?? DateTime.MaxValue;

        var categoryDonutCharts =
            await chartsService.CalculateCategoryDonutChart(query.UserId, from, to, cancellationToken);

        return categoryDonutCharts.Match(
            value => new ChartsResult(value),
            ErrorOr<ChartsResult>.From
        );
    }
}