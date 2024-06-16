namespace ExpenseManager.Application.Statistics.Common;

public sealed record GetBalanceResult(
    decimal TotalBalance,
    decimal TotalExpenses,
    decimal TotalIncome
);