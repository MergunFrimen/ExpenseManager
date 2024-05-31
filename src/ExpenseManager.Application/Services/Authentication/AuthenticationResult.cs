using ExpenseManager.Domain.Entities;

namespace ExpenseManager.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token 
);