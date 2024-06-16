using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Application.Statistics.Queries.GetBalance;
using ExpenseManager.Presentation.Contracts.Statistics;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public class StatisticsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(GetBalanceRequest Request, Guid UserId), GetBalanceQuery>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<GetBalanceResult, GetBalanceResponse>()
            .Map(dest => dest, src => src);
        
        config.NewConfig<ChartsResult, ChartsResponse>()
            .Map(dest => dest, src => src);
    }
}