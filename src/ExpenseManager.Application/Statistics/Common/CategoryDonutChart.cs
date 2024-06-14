namespace ExpenseManager.Application.Statistics.Common;

public sealed record CategoryDonutChart(
    ExpenseCategoryDonutChart ExpenseCategoryDonutChart,
    IncomeCategoryDonutChart IncomeCategoryDonutChart
);

public sealed record ExpenseCategoryDonutChart(
    decimal Total,
    List<ExpenseCategoryTotal> Categories
);

public sealed record IncomeCategoryDonutChart(
    decimal Total,
    List<IncomeCategoryTotal> Categories
);

public sealed record ExpenseCategoryTotal(
    string Category,
    decimal Total
);

public sealed record IncomeCategoryTotal(
    string Category,
    decimal Total
);