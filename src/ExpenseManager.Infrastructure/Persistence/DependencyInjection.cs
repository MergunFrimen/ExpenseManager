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
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository, TestUserRepository>();
        services.AddScoped<ITransactionRepository, TestTransactionRepository>();
        
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");
        services.AddDbContext<ExpenseManagerDbContext>(options =>
            options.UseNpgsql(connectionString)
        );

        return services;
    }
}