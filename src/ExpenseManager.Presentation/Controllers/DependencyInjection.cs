using Asp.Versioning;

namespace ExpenseManager.Presentation.Controllers;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        });
            
        // swagger
        //     .AddApiExplorer(options =>
        // {
        //     options.GroupNameFormat = "'v'V";
        //     options.SubstituteApiVersionInUrl = true;
        // });

        return services;
    }
}