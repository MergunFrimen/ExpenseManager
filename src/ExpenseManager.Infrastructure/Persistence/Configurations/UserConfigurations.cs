using ExpenseManager.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureTransactionTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> usersBuilder)
    {
        usersBuilder
            .ToTable("Users");

        usersBuilder
            .HasKey(u => u.Id);

        usersBuilder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion<Guid>();

        usersBuilder
            .Property(u => u.FirstName)
            .HasMaxLength(50);

        usersBuilder
            .Property(u => u.LastName)
            .HasMaxLength(50);

        usersBuilder
            .Property(u => u.Email)
            .HasMaxLength(150);

        usersBuilder
            .Property(u => u.Password);
    }

    private void ConfigureTransactionTable(EntityTypeBuilder<User> usersBuilder)
    {
        usersBuilder.OwnsMany(user => user.Transactions, transactionsBuilder =>
        {
            transactionsBuilder
                .ToTable("Transactions");
            transactionsBuilder
                .HasKey("Id", "UserId");
            transactionsBuilder
                .WithOwner()
                .HasForeignKey("UserId");
           
            transactionsBuilder
                .Property(s => s.Id)
                .ValueGeneratedNever()
                .HasConversion<Guid>();
            transactionsBuilder
                .Property(s => s.UserId)
                .ValueGeneratedNever()
                .HasConversion<Guid>();
            transactionsBuilder
                .Property(s => s.Type);
            transactionsBuilder
                .Property(s => s.Category);
            transactionsBuilder
                .Property(s => s.Description);
            transactionsBuilder
                .Property(s => s.Price);
            transactionsBuilder
                .Property(s => s.Date);
        });
    }
}