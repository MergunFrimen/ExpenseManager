using ExpenseManager.Application.Common.Extensions;
using ExpenseManager.Domain.Categories;
using FluentValidation;

namespace ExpenseManager.Application.Categories.Queries.SearchCategories;

public class SearchCategoriesQueryValidator : AbstractValidator<SearchCategoriesQuery>
{
    public SearchCategoriesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).CategoryName();
    }
}