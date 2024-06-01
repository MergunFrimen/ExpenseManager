using ExpenseManager.Api;
using ExpenseManager.Api.Middleware;
using ExpenseManager.Application;
using ExpenseManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApi();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        // swagger
        // app.UseSwagger();
        // app.UseSwaggerUI();
    }
    
    app.UseHttpsRedirection();
    app.MapControllers();
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    
    app.Run();
}
