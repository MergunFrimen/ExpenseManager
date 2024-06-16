using ExpenseManager.Application.Import.Commands;
using ExpenseManager.Application.Import.Common;
using ExpenseManager.Presentation.Contracts.Import;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class ImportMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(ImportRequest Request, Guid UserId), ImportCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);
        
        config.NewConfig<ImportResult, ImportResponse>()
            .Map(dest => dest, src => src);
    }
}