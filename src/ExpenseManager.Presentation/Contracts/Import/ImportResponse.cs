namespace ExpenseManager.Presentation.Contracts.Import;

public sealed record ImportResponse(
    int AmountTransactionAdded,
    int AmountCategoryAdded
);