using System.Text;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Infrastructure.Authentication;
using ExpenseManager.Infrastructure.Persistence;
using ExpenseManager.Infrastructure.Persistence.Repositories;
using ExpenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

    public static IServiceCollection AddAuth(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        var jwtSettings = new JwtSettings();
        configurationManager.GetSection(JwtSettings.SectionName).Bind(jwtSettings);


        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddDbContext<ExpenseManagerDbContext>(options =>
            options.UseNpgsql(configurationManager.GetConnectionString("DefaultConnection"))
        );

        return services;
    }
}