using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Ledger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public class LedgerConfigurations : IEntityTypeConfiguration<Ledger>
{
    public void Configure(EntityTypeBuilder<Ledger> builder)
    {
        ConfigureLedgersTable(builder);
        ConfigureTransactionsTable(builder);
    }

    private void ConfigureLedgersTable(EntityTypeBuilder<Ledger> ledgerBuilder)
    {
        ledgerBuilder
            .ToTable("Ledgers");
        ledgerBuilder
            .HasKey(ledger => ledger.Id);

        ledgerBuilder
            .Property(ledger => ledger.Id)
            .HasColumnName("LedgerId");
        ledgerBuilder
            .Property(ledger => ledger.UserId)
            .IsRequired();
        ledgerBuilder
            .Property(ledger => ledger.Name)
            .HasMaxLength(100)
            .IsRequired();
    }

    private void ConfigureTransactionsTable(EntityTypeBuilder<Ledger> ledgerBuilder)
    {
        ledgerBuilder.OwnsMany(ledger => ledger.Transactions, transactionBuilder =>
        {
            transactionBuilder
                .ToTable("Transactions");
            transactionBuilder
                .WithOwner()
                .HasForeignKey("LedgerId");
            transactionBuilder
                .HasKey("TransactionId", "LedgerId");

            transactionBuilder
                .Property(transaction => transaction.Id)
                .HasColumnName("TransactionId");
            transactionBuilder
                .Property(transaction => transaction.Description)
                .HasMaxLength(100)
                .IsRequired();
            transactionBuilder
                .Property(transaction => transaction.Price)
                .IsRequired();
            transactionBuilder
                .Property(transaction => transaction.Date)
                .IsRequired();
            
            transactionBuilder.OwnsOne(transaction => transaction.Category, categoryBuilder =>
            {
                categoryBuilder
                    .Property(category => category.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        });

        ledgerBuilder.Metadata.FindNavigation(nameof(Ledger.Transactions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}