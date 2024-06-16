using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Import.Common;

namespace ExpenseManager.Application.Import.Commands;

public class ImportCommandHandler(
    IImportService importService
)
    : ICommandHandler<ImportCommand, ImportResult>
{
    public async Task<ErrorOr<ImportResult>> Handle(ImportCommand command, CancellationToken cancellationToken)
    {
        var result = await importService.ImportAsync(command, cancellationToken);
        if (result.IsError)
            return result.Errors;

        return result.Value;
    }
}