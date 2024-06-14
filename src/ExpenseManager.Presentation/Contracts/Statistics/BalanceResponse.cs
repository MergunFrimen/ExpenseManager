namespace ExpenseManager.Presentation.Contracts.Statistics;

public sealed record BalanceResponse(
    decimal TotalBalance,
    decimal TotalIncome,
    decimal TotalExpense
);