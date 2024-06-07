using ExpenseManager.Domain.Transactions;
using ExpenseManager.Domain.Users;
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
            .HasConversion<Guid>();
        transactionsBuilder
            .Property(s => s.UserId)
            .ValueGeneratedNever()
            .HasConversion<Guid>();
        transactionsBuilder
            .Property(s => s.CategoryId)
            .ValueGeneratedNever()
            .HasConversion<Guid>();
        transactionsBuilder
            .Property(s => s.Type);
        transactionsBuilder
            .Property(s => s.Description);
        transactionsBuilder
            .Property(s => s.Price);
        transactionsBuilder
            .Property(s => s.Date);
    }
}