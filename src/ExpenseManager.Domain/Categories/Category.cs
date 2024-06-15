using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Domain.Categories;

public sealed class Category : Entity
{
    public const int NameMaxLength = 50;

    private Category(
        Guid id,
        string name,
        User user
    ) : base(id)
    {
        Name = name;
        User = user;
    }

    private Category()
    {
    }

    public string Name { get; private set; }
    public User User { get; private set; }

    public static Category Create(
        Guid? id,
        string name,
        User user
    )
    {
        var transaction = new Category(
            id ?? Guid.NewGuid(),
            name,
            user
        );

        return transaction;
    }
}