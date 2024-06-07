using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseManagerDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(user => user.Email == email, cancellationToken);

        return user;
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        var newUser = await dbContext.Users.AddAsync(user, cancellationToken);

        return newUser.Entity;
    }
}