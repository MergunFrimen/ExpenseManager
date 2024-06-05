using ExpenseManager.Domain.Ledger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseManager.Infrastructure.Persistence;

public class ExpenseManagerDbContext(DbContextOptions<ExpenseManagerDbContext> options) : DbContext(options)
{
    public DbSet<Ledger> Ledgers { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        // Disable auto increment for primary keys
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(entityType => entityType.GetProperties())
            .Where(property => property.IsPrimaryKey())
            .ToList()
            .ForEach(property => property.ValueGenerated = ValueGenerated.Never);
    }
}