using FluentValidation;

namespace ExpenseManager.Application.Export.Queries;

public class ExportQueryValidator : AbstractValidator<ExportQuery>
{
    public ExportQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}