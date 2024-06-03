using ExpenseManager.Application.Authentication.Commands.Register;
using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Authentication.Queries.Login;
using ExpenseManager.Contracts.Authentication;
using Mapster;

namespace ExpenseManager.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}