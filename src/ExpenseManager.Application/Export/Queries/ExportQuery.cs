using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Export.Common;

namespace ExpenseManager.Application.Export.Queries;

public record ExportQuery(
    Guid UserId
) : IQuery<ExportResult>;