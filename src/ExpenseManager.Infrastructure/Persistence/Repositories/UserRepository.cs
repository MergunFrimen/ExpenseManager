using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseManagerDbContext dbContext) : IUserRepository
{
    public User? GetUserByEmail(string email)
    {
        var user = dbContext.Users.SingleOrDefault(user => user.Email == email);

        return user;
    }

    public void Add(User user)
    {
        dbContext.Users.Add(user);
    }
}