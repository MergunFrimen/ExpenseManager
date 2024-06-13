using ExpenseManager.Application.Common.Extensions;
using ExpenseManager.Domain.Categories;
using FluentValidation;

namespace ExpenseManager.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).CategoryName();
    }
}