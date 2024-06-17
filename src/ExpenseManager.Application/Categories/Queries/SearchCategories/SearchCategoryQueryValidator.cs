using FluentValidation;

namespace ExpenseManager.Application.Categories.Queries.SearchCategories;

public class SearchCategoriesQueryValidator : AbstractValidator<SearchCategoriesQuery>
{
    public SearchCategoriesQueryValidator()
    {
        RuleFor(x => x.Filters).NotEmpty();
    }
}