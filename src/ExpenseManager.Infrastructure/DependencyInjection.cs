using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Services.Authentication;
using ExpenseManager.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}