using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseManagerDbContext dbContext) : IUserRepository
{
    private static readonly List<User> Users = [];

    public User? GetUserByEmail(string email)
    {
        var user = Users.SingleOrDefault(user => user.Email == email);

        return user;
    }

    public void Add(User user)
    {
        Users.Add(user);
    }
}