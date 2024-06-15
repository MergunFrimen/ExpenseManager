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

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(user => user.FirstName)
            .HasMaxLength(User.FirstNameMaxLength)
            .IsRequired();
        builder.Property(user => user.LastName)
            .HasMaxLength(User.LastNameMaxLength)
            .IsRequired();
        builder.Property(user => user.Email)
            .HasMaxLength(User.EmailMaxLength)
            .IsRequired();
        builder.Property(user => user.Password)
            .IsRequired();
    }
}