using ExpenseManager.Application.Common.Interfaces.Authentication;

namespace ExpenseManager.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator tokenGenerator): IAuthenticationService
{
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user already exists with arguments
        
        // Generate user (gets the user's ID)
        var userId = Guid.NewGuid();
        
        // Create JWT token
        var token = tokenGenerator.GenerateToken(userId, firstName, lastName);
        
        return new AuthenticationResult(userId, firstName, lastName, email, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "firstName", "lastName", email, "token");
    }
}