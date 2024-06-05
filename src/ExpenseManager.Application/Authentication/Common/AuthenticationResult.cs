using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Authentication.Common;

public sealed record AuthenticationResult(
    User User,
    string Token
);