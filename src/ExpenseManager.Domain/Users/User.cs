using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Users;

public sealed class User : Entity
{
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

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password)
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