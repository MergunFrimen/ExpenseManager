using ExpenseManager.Application.Export.Queries;
using FluentValidation;

namespace ExpenseManager.Application.Import.Commands;

public class ImportCommandValidator : AbstractValidator<ImportCommand>
{
    public ImportCommandValidator()
    {
        RuleFor(x => x.Transactions).NotEmpty();
        RuleFor(x => x.Categories).NotEmpty();
    }
}