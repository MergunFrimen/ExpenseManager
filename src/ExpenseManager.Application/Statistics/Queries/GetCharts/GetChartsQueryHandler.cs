using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Statistics.Queries.GetCharts;

public class GetChartsQueryHandler(
    IChartsService chartsService)
    : IQueryHandler<GetChartsQuery, ChartsResult>
{
    public async Task<ErrorOr<ChartsResult>> Handle(GetChartsQuery query, CancellationToken cancellationToken)
    {
        var expenseCategoryDonutChart =
            await chartsService.CalculateCategoryDonutChart(query.UserId, TransactionType.Expense, cancellationToken);
        var incomeCategoryDonutChart =
            await chartsService.CalculateCategoryDonutChart(query.UserId, TransactionType.Income, cancellationToken);

        return new ChartsResult(expenseCategoryDonutChart, incomeCategoryDonutChart);
    }
}