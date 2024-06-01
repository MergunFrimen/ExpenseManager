using ExpenseManager.Api.Errors;
using ExpenseManager.Api.Filters;
using ExpenseManager.Api.Middleware;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ExpenseManager.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddControllers();
        
        services.AddSingleton<ProblemDetailsFactory, ExpenseManagerProblemDetailsFactory>();
        
        // services.AddControllers(configure => configure.Filters.Add<ErrorHandlingFilterAttribute>());
        
        return services;
    }   
}