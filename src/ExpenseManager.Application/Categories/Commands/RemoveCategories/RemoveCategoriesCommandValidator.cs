using FluentValidation;

namespace ExpenseManager.Application.Categories.Commands.RemoveCategories;

public class RemoveCategoriesCommandValidator : AbstractValidator<RemoveCategoriesCommand>
{
    public RemoveCategoriesCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.CategoryIds);
    }
}

