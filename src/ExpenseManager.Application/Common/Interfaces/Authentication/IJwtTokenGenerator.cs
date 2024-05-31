using ExpenseManager.Domain.Entities;

namespace ExpenseManager.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}