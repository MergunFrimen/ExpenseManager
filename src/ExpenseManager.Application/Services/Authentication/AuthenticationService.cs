using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Entities;

namespace ExpenseManager.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    : IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user already exists with arguments
        if (userRepository.GetUserByEmail(email) is not null) throw new Exception("User with email exists");

        // Generate user (gets the user's ID)
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        var userId = userRepository.Add(user);

        // Create JWT token
        var token = tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        if (userRepository.GetUserByEmail(email) is not { } user) throw new Exception("User doesn't exist");

        if (user.Password != password) throw new Exception("Password incorrect");

        var token = tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}