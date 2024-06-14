using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Infrastructure.Persistence;

namespace ExpenseManager.Infrastructure.Services;

public class ChartsService(ExpenseManagerDbContext dbContext) : IChartsService
{
    public async Task<ErrorOr<CategoryDonutChart>> CalculateCategoryDonutChart(Guid userId, DateTime from, DateTime to,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}