using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        ConfigureTransactionTable(builder);
    }

    private void ConfigureTransactionTable(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(transaction => new
        {
            transaction.Id,
        });
        
        builder.Property(transaction => transaction.Id)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(transaction => transaction.Description)
            .HasMaxLength(Transaction.DescriptionMaxLength)
            .IsRequired();
        builder.Property(transaction => transaction.Amount)
            .IsRequired();
        
        // creates foreign key referencing User table
        builder
            .HasOne(x => x.User)
            .WithMany()
            .IsRequired();
        
        // creates TransactionCategory table for Many-to-Many relationship
        builder
            .HasMany(transaction => transaction.Categories)
            .WithMany();
    }
}