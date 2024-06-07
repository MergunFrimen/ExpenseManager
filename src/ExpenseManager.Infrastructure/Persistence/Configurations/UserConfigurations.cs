using ExpenseManager.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
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
            .HasConversion<Guid>()
            .IsRequired();
        usersBuilder
            .Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        usersBuilder
            .Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();
        usersBuilder
            .Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired();
        usersBuilder
            .Property(u => u.Password)
            .IsRequired();
    }
}