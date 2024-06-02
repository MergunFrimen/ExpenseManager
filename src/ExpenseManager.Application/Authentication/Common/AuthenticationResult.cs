using ExpenseManager.Domain.Entities;

namespace ExpenseManager.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);