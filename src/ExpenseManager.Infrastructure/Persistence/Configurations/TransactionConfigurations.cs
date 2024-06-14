using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        ConfigureTransactionTable(builder);
        ConfigureCategoryTable(builder);
    }

    private void ConfigureTransactionTable(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        
        builder.HasKey("Id", "UserId");
        
        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion<Guid>()
            .IsRequired();
        
        builder.Property(s => s.UserId)
            .ValueGeneratedNever()
            .HasConversion<Guid>()
            .IsRequired();
        
        builder.Property(s => s.Type)
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(s => s.Description)
            .HasMaxLength(Transaction.DescriptionMaxLength)
            .IsRequired();
        
        builder.Property(s => s.Amount)
            .IsRequired();
        
        builder.Property(s => s.Date);
    }

    private void ConfigureCategoryTable(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasMany(transaction => transaction.Categories)
            .WithMany(category => category.Transactions)
            .UsingEntity(typeBuilder => typeBuilder.ToTable("TransactionCategory"));
    }
}