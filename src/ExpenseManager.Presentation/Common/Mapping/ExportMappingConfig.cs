using ExpenseManager.Application.Export.Common;
using ExpenseManager.Presentation.Contracts.Export;
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