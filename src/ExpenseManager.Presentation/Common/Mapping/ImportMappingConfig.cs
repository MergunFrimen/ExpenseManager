using ExpenseManager.Application.Export.Common;
using ExpenseManager.Application.Import.Commands;
using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransactions;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Application.Transactions.Queries.SearchTransactions;
using ExpenseManager.Presentation.Contracts.Export;
using ExpenseManager.Presentation.Contracts.Import;
using ExpenseManager.Presentation.Contracts.Transactions;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class ImportMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(ImportRequest Request, Guid UserId), ImportCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
    }
}