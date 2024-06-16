using FluentValidation;

namespace ExpenseManager.Application.Import.Commands;

public class ImportCommandValidator : AbstractValidator<ImportCommand>
{
    public ImportCommandValidator()
    {
        RuleFor(x => x.Transactions).NotNull();
        RuleFor(x => x.Categories).NotNull();
    }
}