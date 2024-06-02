using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Entities;

namespace ExpenseManager.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    : IAuthenticationService
{
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if (userRepository.GetUserByEmail(email) is not null) return Errors.User.DuplicateEmail;

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        var userId = userRepository.Add(user);

        var token = tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if (userRepository.GetUserByEmail(email) is not { } user) return Errors.Authentication.InvalidCredentials;

        if (user.Password != password) return Errors.Authentication.InvalidCredentials;

        var token = tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}