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

    private void ConfigureTransactionTable(EntityTypeBuilder<Transaction> transactionsBuilder)
    {
        transactionsBuilder
            .ToTable("Transactions");
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
            .Property(s => s.CategoryId)
            .ValueGeneratedNever()
            .HasConversion<Guid>()
            .IsRequired();
        transactionsBuilder
            .Property(s => s.Type)
            .HasConversion<int>()
            .IsRequired();
        transactionsBuilder
            .Property(s => s.Description)
            .HasMaxLength(Transaction.DescriptionMaxLength)
            .IsRequired();
        transactionsBuilder
            .Property(s => s.Amount)
            .IsRequired();
        transactionsBuilder
            .Property(s => s.Date)
            .HasConversion(
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc),
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc))
            .IsRequired();
    }
}