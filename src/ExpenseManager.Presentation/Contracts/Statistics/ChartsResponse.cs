using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Presentation.Contracts.Statistics;

public sealed record ChartsResponse(
    List<CategoryTotal> ExpenseCategoryDonutChart,
    List<CategoryTotal> IncomeCategoryDonutChart
);
