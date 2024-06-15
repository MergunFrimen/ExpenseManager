using ExpenseManager.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoryTable(builder);
    }

    private void ConfigureCategoryTable(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(category => new
        {
            category.Id,
        });
        
        builder.Property(category => category.Id)
            .ValueGeneratedNever()
            .IsRequired();
        builder.Property(category => category.Name)
            .HasMaxLength(Category.NameMaxLength)
            .IsRequired();
        
        // creates foreign key referencing User table
        builder
            .HasOne(x => x.User)
            .WithMany()
            .IsRequired();
    }
}