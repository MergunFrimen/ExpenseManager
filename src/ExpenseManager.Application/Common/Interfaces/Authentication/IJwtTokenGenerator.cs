using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}