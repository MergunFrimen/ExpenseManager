using ExpenseManager.Domain.Entities;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    Guid Add(User user);
}