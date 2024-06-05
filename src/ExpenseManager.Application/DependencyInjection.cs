using System.Reflection;
using ExpenseManager.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddMediatR(options =>
        // {
        //     options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        //
        //     // options.AddOpenBehavior(typeof(IPipelineBehavior<,>));
        //     options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        // });
        //
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // // services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}