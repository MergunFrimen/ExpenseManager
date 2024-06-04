using ExpenseManager.Domain.UserAggregate;

namespace ExpenseManager.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}