namespace ExpenseManager.Presentation.Contracts.Statistics;

public sealed record GetBalanceResponse(
    decimal TotalBalance,
    decimal TotalExpenses,
    decimal TotalIncome
);