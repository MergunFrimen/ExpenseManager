namespace ExpenseManager.Application.Statistics.Common;

public sealed record GetPriceRangeResult(
    decimal MinPrice,
    decimal MaxPrice
);