using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}