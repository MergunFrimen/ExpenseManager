using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);