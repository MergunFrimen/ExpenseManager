using ExpenseManager.Application.Common.Extensions;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using FluentValidation;

namespace ExpenseManager.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).CategoryName();
    }
}