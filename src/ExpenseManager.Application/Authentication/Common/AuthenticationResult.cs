using ExpenseManager.Domain.UserAggregate;

namespace ExpenseManager.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);