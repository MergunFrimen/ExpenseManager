using ExpenseManager.Domain.Categories;
using FluentValidation;

namespace ExpenseManager.Application.Categories.Queries.FindCategories;

public class SearchCategoriesQueryValidator : AbstractValidator<FindCategoriesQuery>
{
    public SearchCategoriesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Category.NameMaxLength);
    }
}