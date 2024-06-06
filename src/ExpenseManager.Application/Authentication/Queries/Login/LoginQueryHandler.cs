using ErrorOr;
using ExpenseManager.Application.Authentication.Common;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    IJwtTokenGenerator tokenGenerator,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : IQueryHandler<LoginQuery, AuthenticationResult>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(query.Email, cancellationToken);

        // don't leak information about whether the user exists
        if (user is null)
            return Errors.Authentication.InvalidCredentials;

        if (!passwordHasher.Verify(query.Password, user.Password))
            return Errors.Authentication.InvalidCredentials;

        var token = tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}