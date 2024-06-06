using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Transaction> _transactions = null!;

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password,
        List<Transaction> transactions
    )
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        _transactions = transactions;
    }

    private User()
    {
    }

    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password,
        List<Transaction>? transactions)
    {
        return new User(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            password,
            transactions ?? []);
    }
}