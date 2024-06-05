using ExpenseManager.Domain.Ledger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public class LedgerConfigurations : IEntityTypeConfiguration<Ledger>
{
    public void Configure(EntityTypeBuilder<Ledger> builder)
    {
        ConfigureLedgerTable(builder);
        ConfigureTransactionsTable(builder);
    }

    public void ConfigureLedgerTable(EntityTypeBuilder<Ledger> builder)
    {
        builder.ToTable("Ledgers");
        builder.HasKey(ledger => ledger.Id);
        builder.Property(ledger => ledger.Id).IsRequired().HasConversion<Guid>().ValueGeneratedNever();
        builder.Property(ledger => ledger.UserId).IsRequired().HasConversion<Guid>().ValueGeneratedNever();
        builder.Property(ledger => ledger.Name).IsRequired().HasMaxLength(100);
    }

    public void ConfigureTransactionsTable(EntityTypeBuilder<Ledger> ledgerBuilder)
    {
        ledgerBuilder.OwnsMany(ledger => ledger.Transactions, transactionBuilder =>
        {
            transactionBuilder.ToTable("Transactions");
            transactionBuilder.WithOwner().HasForeignKey("LedgerId");
            transactionBuilder.HasKey("Id", "LedgerId");

            transactionBuilder.Property(transaction => transaction.Id)
                .HasColumnName("TransactionId")
                .ValueGeneratedNever();
            transactionBuilder.Property(transaction => transaction.Description)
                .HasMaxLength(100)
                .IsRequired();
            transactionBuilder.Property(transaction => transaction.Date)
                .IsRequired();
            transactionBuilder.Property(transaction => transaction.Price)
                .IsRequired();
            transactionBuilder.OwnsOne(transaction => transaction.Category, categoryBuilder =>
            {
                categoryBuilder.Property(category => category.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        });

        ledgerBuilder.Metadata.FindNavigation(nameof(Ledger.Transactions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}