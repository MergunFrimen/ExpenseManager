using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Contracts.Authentication;
using Mapster;

namespace ExpenseManager.Api.Common.Mapping;

public class AuthenticationMappingConfig: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}   