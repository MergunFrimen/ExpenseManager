using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseManagerDbContext dbContext) : IUserRepository
{
    public async Task<ErrorOr<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Users.FirstOrDefaultAsync(c => c.Id == id, cancellationToken) is not { } user)
            return Errors.User.NotFound;

        return user;
    }

    public async Task<ErrorOr<List<User>>> FindAsync(Expression<Func<User, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var users = await dbContext.Users.Where(predicate).ToListAsync(cancellationToken);

        return users;
    }

    public async Task<ErrorOr<User>> AddAsync(User user, CancellationToken cancellationToken)
    {
        var newUser = await dbContext.Users.AddAsync(user, cancellationToken);

        return newUser.Entity;
    }
}