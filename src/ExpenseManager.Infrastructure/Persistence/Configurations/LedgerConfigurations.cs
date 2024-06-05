// using ExpenseManager.Domain.Ledger;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace ExpenseManager.Infrastructure.Persistence.Configurations;
//
// public class LedgerConfigurations : IEntityTypeConfiguration<Ledger>
// {
//     public void Configure(EntityTypeBuilder<Ledger> builder)
//     {
//         ConfigureLedgersTable(builder);
//         ConfigureTransactionsTable(builder);
//     }
//
//     private void ConfigureLedgersTable(EntityTypeBuilder<Ledger> ledgerBuilder)
//     {
//         ledgerBuilder
//             .ToTable("Ledgers");
//         ledgerBuilder
//             .HasKey(ledger => ledger.Id);
//
//         ledgerBuilder
//             .Property(ledger => ledger.Id);
//         ledgerBuilder
//             .Property(ledger => ledger.UserId)
//             .IsRequired();
//         ledgerBuilder
//             .Property(ledger => ledger.Name)
//             .HasMaxLength(100)
//             .IsRequired();
//     }
//
//     private void ConfigureTransactionsTable(EntityTypeBuilder<Ledger> ledgerBuilder)
//     {
//         ledgerBuilder.OwnsMany(ledger => ledger.Transactions, transactionBuilder =>
//         {
//             transactionBuilder
//                 .ToTable("Transactions");
//             transactionBuilder
//                 .WithOwner()
//                 .HasForeignKey("Id");
//             transactionBuilder
//                 .HasKey("Id", "LedgerId");
//
//             transactionBuilder
//                 .Property(transaction => transaction.Id);
//             transactionBuilder
//                 .Property(transaction => transaction.LedgerId)
//                 .ValueGeneratedNever()
//                 .IsRequired();
//             transactionBuilder
//                 .Property(transaction => transaction.Type)
//                 .HasMaxLength(50)
//                 .IsRequired();
//             transactionBuilder
//                 .Property(transaction => transaction.Category)
//                 .HasMaxLength(50)
//                 .IsRequired();
//             transactionBuilder
//                 .Property(transaction => transaction.Description)
//                 .HasMaxLength(100)
//                 .IsRequired();
//             transactionBuilder
//                 .Property(transaction => transaction.Price)
//                 .IsRequired();
//             transactionBuilder
//                 .Property(transaction => transaction.Date)
//                 .IsRequired();
//         });
//
//         ledgerBuilder.Metadata.FindNavigation(nameof(Ledger.Transactions))!
//             .SetPropertyAccessMode(PropertyAccessMode.Field);
//     }
// }