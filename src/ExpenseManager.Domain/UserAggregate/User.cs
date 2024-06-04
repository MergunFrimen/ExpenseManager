using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.TransactionAggregate.ValueObjects;
using ExpenseManager.Domain.UserAggregate.ValueObjects;

namespace ExpenseManager.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private readonly List<TransactionId> _transactionIds = [];

    public User(UserId id, string firstName, string lastName, string email, string password)
        : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public new UserId Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public string Email { get; private set; }

    // TODO: should be hashed
    public string Password { get; private set; }
    public IReadOnlyList<TransactionId> TransactionIds => _transactionIds.AsReadOnly();

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