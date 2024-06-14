namespace ExpenseManager.Presentation.Contracts.Statistics;

public sealed record ChartsRequest(
    DateTime? From,
    DateTime? To,
    string? GroupBy
);