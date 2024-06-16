using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetCharts;

public sealed record GetChartsQuery(
    Guid UserId
) : IQuery<ChartsResult>;