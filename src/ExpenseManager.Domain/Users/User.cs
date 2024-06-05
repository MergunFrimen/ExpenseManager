using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users.ValueObjects;

namespace ExpenseManager.Domain.Users;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password)
        : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public new UserId Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }
    // TODO: this shouldn't be here

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        return new User(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password);
    }
}