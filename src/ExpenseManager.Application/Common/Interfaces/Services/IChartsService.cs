using ErrorOr;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Common.Interfaces.Services;

public interface IChartsService
{
    Task<ErrorOr<CategoryDonutChart>> CalculateCategoryDonutChart(Guid userId, DateTime from, DateTime to,
        CancellationToken cancellationToken);
}