using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Categories;

public sealed class Category : Entity
{
    private Category(
        Guid id,
        Guid userId,
        string name
    ) : base(id)
    {
        UserId = userId;
        Name = name;
    }

    private Category()
    {
    }

    public Guid UserId { get; private set; }
    public string Name { get; private set; } = null!;
    
    public const int NameMaxLength = 50;

    public static Category Create(
        Guid? id,
        Guid userId,
        string name
    )
    {
        var transaction = new Category(
            id ?? Guid.NewGuid(),
            userId,
            name
        );

        return transaction;
    }
}