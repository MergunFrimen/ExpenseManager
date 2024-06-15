using ExpenseManager.Application.Export.Common;
using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransactions;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Application.Transactions.Queries.SearchTransactions;
using ExpenseManager.Presentation.Contracts.Export;
using ExpenseManager.Presentation.Contracts.Transactions;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class ExportMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ExportResult, ExportResponse>()
            .Map(dest => dest.Transactions, src => src.Transactions)
            .Map(dest => dest.Categories, src => src.Categories);
    }
}