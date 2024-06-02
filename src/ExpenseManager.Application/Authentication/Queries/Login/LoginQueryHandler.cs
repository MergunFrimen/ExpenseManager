using ErrorOr;
using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using MediatR;

namespace ExpenseManager.Application.Authentication.Queries.Login;

public class LoginQueryHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (userRepository.GetUserByEmail(query.Email) is not { } user)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.InvalidCredentials);

        if (user.Password != query.Password)
            return Task.FromResult<ErrorOr<AuthenticationResult>>(Errors.Authentication.InvalidCredentials);

        var token = tokenGenerator.GenerateToken(user);

        return Task.FromResult<ErrorOr<AuthenticationResult>>(new AuthenticationResult(user, token));
    }
}