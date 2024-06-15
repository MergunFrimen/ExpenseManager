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
        // builder.Property(transaction => transaction.UserId)
        //     .ValueGeneratedNever()
        //     .IsRequired();
        
        builder
            .HasOne(x => x.User)
            .WithMany()
            .IsRequired();
        builder
            .HasMany(transaction => transaction.Categories)
            .WithMany();
    }
}