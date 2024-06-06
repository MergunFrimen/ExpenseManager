using ExpenseManager.Presentation.Common.Errors;
using ExpenseManager.Presentation.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ExpenseManager.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ExpenseManagerProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }
}