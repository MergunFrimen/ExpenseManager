using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Users;

public sealed class User : Entity
{
    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        // CreatedDateTime = DateTime.UtcNow;
        // UpdatedDateTime = DateTime.UtcNow;
    }

    // TODO: does ledger have to be aggregate root????
    public Guid LedgerId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    // public DateTime CreatedDateTime { get; private set; }
    // public DateTime UpdatedDateTime { get; private set; }


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
            password);
    }
}