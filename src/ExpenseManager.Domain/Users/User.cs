using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Users;

public sealed class User : Entity
{
    public const int FirstNameMaxLength = 50;
    public const int LastNameMaxLength = 50;
    public const int EmailMaxLength = 150;

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password
    )
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    private User()
    {
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password
    )
    {
        return new User(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            password
        );
    }
}