using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.UserAggregate;

namespace ExpenseManager.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = [];

    public User? GetUserByEmail(string email)
    {
        var user = Users.SingleOrDefault(user => user.Email == email);

        return user;
    }

    public Guid Add(User user)
    {
        Users.Add(user);

        return user.Id;
    }
}