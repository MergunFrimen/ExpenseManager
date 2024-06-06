using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Infrastructure.Authentication;
using ExpenseManager.Infrastructure.Persistence;
using ExpenseManager.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services
            .AddAuth(configurationManager)
            .AddPersistence(configurationManager);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}