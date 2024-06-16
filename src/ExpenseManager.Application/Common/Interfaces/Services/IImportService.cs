using ExpenseManager.Application.Import.Commands;
using ExpenseManager.Application.Import.Common;
using ExpenseManager.Domain.Transactions;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Services;

public interface IImportService
{
     Task<ErrorOr<ImportResult>> ImportAsync(ImportCommand command, CancellationToken cancellationToken);
}