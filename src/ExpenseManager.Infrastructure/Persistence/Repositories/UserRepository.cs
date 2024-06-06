using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseManagerDbContext dbContext) : IUserRepository
{
    private static readonly List<User> Users = [];

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = Users.SingleOrDefault(user => user.Email == email);
        // var user = await dbContext.Users.SingleOrDefaultAsync(user => user.Email == email, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        Users.Add(user);
        // await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}