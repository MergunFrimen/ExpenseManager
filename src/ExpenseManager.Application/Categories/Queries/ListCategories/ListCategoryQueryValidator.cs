using FluentValidation;

namespace ExpenseManager.Application.Categories.Queries.ListCategories;

public class ListCategoriesQueryValidator : AbstractValidator<ListCategoriesQuery>
{
    public ListCategoriesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}