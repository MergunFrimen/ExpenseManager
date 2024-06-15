using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Categories;

public sealed class Category : Entity
{
    public const int NameMaxLength = 50;

    private Category(
        Guid id,
        string name,
        Guid userId
    ) : base(id)
    {
        Name = name;
        UserId = userId;
    }

    private Category()
    {
    }

    public string Name { get; private set; }
    public Guid UserId { get; private set; }

    public static Category Create(
        Guid? id,
        string name,
        Guid userId
    )
    {
        var transaction = new Category(
            id ?? Guid.NewGuid(),
            name,
            userId
        );

        return transaction;
    }
}