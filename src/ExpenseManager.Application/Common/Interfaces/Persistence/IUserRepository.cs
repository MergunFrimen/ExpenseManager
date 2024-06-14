using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<ErrorOr<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<List<User>>> FindAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    Task<ErrorOr<User>> AddAsync(User user, CancellationToken cancellationToken);
}