namespace ExpenseManager.Presentation.Contracts.Statistics;

public sealed record BalanceRequest(
    DateTime? From,
    DateTime? To
);