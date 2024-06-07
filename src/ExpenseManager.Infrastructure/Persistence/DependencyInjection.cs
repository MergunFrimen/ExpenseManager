using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Infrastructure.Persistence.Interceptors;
using ExpenseManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        services.AddScoped<ICategoryRepository, TestCategoryRepository>();
        services.AddScoped<IUserRepository, TestUserRepository>();
        services.AddScoped<ITransactionRepository, TestTransactionRepository>();

        // services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddDbContext<ExpenseManagerDbContext>(options =>
            options.UseNpgsql("Server=127.0.0.1;UserName=postgres;Password=postgres;Database=ExpenseManager;Port=5432")
        );

        return services;
    }
}