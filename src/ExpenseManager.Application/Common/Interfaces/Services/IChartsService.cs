using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

namespace ExpenseManager.Application.Common.Interfaces.Services;

public interface IChartsService
{
    Task<List<CategoryTotal>> CalculateCategoryDonutChart(Guid userId, TransactionType type,
        CancellationToken cancellationToken);
}