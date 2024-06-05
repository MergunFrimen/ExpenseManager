using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;

namespace ExpenseManager.Application.Authentication.Queries.Login;

public sealed record LoginQuery(
    string Email,
    string Password
) : IQuery<AuthenticationResult>;