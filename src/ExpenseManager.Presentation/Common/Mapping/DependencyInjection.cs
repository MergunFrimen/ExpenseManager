using System.Reflection;
using Mapster;

namespace ExpenseManager.Presentation.Common.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        services.AddMapster();

        return services;
    }
}