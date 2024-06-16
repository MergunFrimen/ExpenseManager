namespace ExpenseManager.Application.Statistics.Common;

public sealed record ChartsResult(
    List<CategoryTotal> ExpenseCategoryDonutChart,
    List<CategoryTotal> IncomeCategoryDonutChart
);