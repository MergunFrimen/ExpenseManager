using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}