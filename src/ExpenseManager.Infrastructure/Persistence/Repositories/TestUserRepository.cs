using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TestUserRepository : IUserRepository
{
    private static readonly List<User> Users = [];

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = Users.SingleOrDefault(user => user.Email == email);

        return Task.FromResult(user);
    }

    public Task AddAsync(User user, CancellationToken cancellationToken)
    {
        Users.Add(user);

        return Task.CompletedTask;
    }
}