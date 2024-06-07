using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Transactions;
using ExpenseManager.Domain.Users;
using ExpenseManager.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseManager.Infrastructure.Persistence;

public class ExpenseManagerDbContext(
    DbContextOptions<ExpenseManagerDbContext> options,
    PublishDomainEventsInterceptor eventsInterceptor) : DbContext(options)
{
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Transaction> Transactions { get; init; } = null!;
    public DbSet<Category> Categories { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ExpenseManagerDbContext).Assembly);

        // Disable auto increment for primary keys
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(entityType => entityType.GetProperties())
            .Where(property => property.IsPrimaryKey())
            .ToList()
            .ForEach(property => property.ValueGenerated = ValueGenerated.Never);

        // Never store domain events in the database
        modelBuilder
            .Ignore<List<IDomainEvent>>();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(eventsInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}