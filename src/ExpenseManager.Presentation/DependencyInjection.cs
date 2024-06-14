using ExpenseManager.Presentation.Common.Errors;
using ExpenseManager.Presentation.Common.Mapping;
using ExpenseManager.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ExpenseManager.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddApi();

        services.AddSingleton<ProblemDetailsFactory, ExpenseManagerProblemDetailsFactory>();
        services.AddMappings();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }
}