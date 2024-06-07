using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Category;

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

    public static Category Create(
        Guid userId,
        string name
    )
    {
        var transaction = new Category(
            Guid.NewGuid(),
            userId,
            name
        );
        
        return transaction;
    }
}