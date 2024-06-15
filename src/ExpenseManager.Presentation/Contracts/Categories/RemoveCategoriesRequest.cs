namespace ExpenseManager.Presentation.Contracts.Categories;

public sealed record RemoveCategoriesRequest(
    List<string> CategoryIds
);