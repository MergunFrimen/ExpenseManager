using FluentValidation;

namespace ExpenseManager.Application.Categories.Queries;

public class ListCategoriesQueryValidator : AbstractValidator<ListCategoriesQuery>
{
    public ListCategoriesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}