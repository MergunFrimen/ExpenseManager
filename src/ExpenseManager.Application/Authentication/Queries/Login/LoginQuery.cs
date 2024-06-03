using ErrorOr;
using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using MediatR;

namespace ExpenseManager.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;