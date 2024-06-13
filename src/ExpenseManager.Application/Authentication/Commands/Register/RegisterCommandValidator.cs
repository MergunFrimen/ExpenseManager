using ExpenseManager.Application.Common.Extensions;
using ExpenseManager.Domain.Users;
using FluentValidation;

namespace ExpenseManager.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(User.FirstNameMaxLength);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(User.LastNameMaxLength);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(User.EmailMaxLength);
        RuleFor(x => x.Password).Password();
    }
}