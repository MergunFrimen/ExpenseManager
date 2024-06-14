namespace ExpenseManager.Application.Statistics.Common;

public sealed record BalanceResult(
    decimal TotalBalance,
    decimal TotalExpense,
    decimal TotalIncome
);