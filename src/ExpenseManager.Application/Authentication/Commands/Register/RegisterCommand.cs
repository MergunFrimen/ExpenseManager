using ErrorOr;
using ExpenseManager.Application.Services.Authentication;
using MediatR;

namespace ExpenseManager.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;