using ExpenseManager.Application;
using ExpenseManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // swagger
    // builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddSwaggerGen();
    
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    
    builder.Services.AddControllers();
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
    
    app.Run();
}
