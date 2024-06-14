using ExpenseManager.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoryTable(builder);
        // ConfigureTransactionTable(builder);
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
            .HasConversion<Guid>()
            .IsRequired();
        transactionsBuilder
            .Property(s => s.UserId)
            .ValueGeneratedNever()
            .HasConversion<Guid>()
            .IsRequired();
        transactionsBuilder
            .Property(s => s.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
    
    private void ConfigureTransactionTable(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasMany(category => category.Transactions)
            .WithMany(transaction => transaction.Categories)
            .UsingEntity(typeBuilder => typeBuilder.ToTable("TransactionCategory"));
    }
}