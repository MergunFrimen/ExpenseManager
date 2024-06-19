namespace ExpenseManager.Presentation.Contracts.Statistics;

public sealed record GetPriceRangeResponse(
    decimal MinPrice,
    decimal MaxPrice
);