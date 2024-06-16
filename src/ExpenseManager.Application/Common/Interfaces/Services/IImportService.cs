using ErrorOr;
using ExpenseManager.Application.Import.Commands;
using ExpenseManager.Application.Import.Common;

namespace ExpenseManager.Application.Common.Interfaces.Services;

public interface IImportService
{
    Task<ErrorOr<ImportResult>> ImportAsync(ImportCommand command, CancellationToken cancellationToken);
}