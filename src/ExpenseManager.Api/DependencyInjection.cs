using ExpenseManager.Api.Filters;
using ExpenseManager.Api.Middleware;

namespace ExpenseManager.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddControllers();

        // services.AddControllers(configure => configure.Filters.Add<ErrorHandlingFilterAttribute>());
        
        return services;
    }   
}