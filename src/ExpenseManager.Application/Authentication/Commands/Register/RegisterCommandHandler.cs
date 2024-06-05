using ErrorOr;
using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Users;
using MediatR;

namespace ExpenseManager.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    public Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (userRepository.GetUserByEmail(command.Email) is not null)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.User.DuplicateEmail);

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password
        );

        userRepository.Add(user);

        var token = tokenGenerator.GenerateToken(user);

        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(user, token));
    }
}