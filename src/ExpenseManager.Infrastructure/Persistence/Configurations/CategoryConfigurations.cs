using ExpenseManager.Domain.Category;
using ExpenseManager.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoryTable(builder);
    }
    
    private void ConfigureCategoryTable(EntityTypeBuilder<Category> transactionsBuilder)
    {
        transactionsBuilder
            .ToTable("Categories");
        transactionsBuilder
            .HasKey("Id", "UserId");

        transactionsBuilder
            .Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion<Guid>();
        transactionsBuilder
            .Property(s => s.UserId)
            .ValueGeneratedNever()
            .HasConversion<Guid>();
        transactionsBuilder
            .Property(s => s.Name)
            .IsRequired();
    }
}