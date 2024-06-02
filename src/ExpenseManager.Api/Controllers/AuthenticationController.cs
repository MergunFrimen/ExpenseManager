using ExpenseManager.Application.Services.Authentication;
using ExpenseManager.Contracts.Authentication;
using ExpenseManager.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

[Route("auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ApiController
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authenticationResult = authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        return authenticationResult.Match(
            value =>
                Ok(MapAuthenticationResponse(value)),
            Problem
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authenticationResult = authenticationService.Login(
            request.Email,
            request.Password
        );

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);

        return authenticationResult.Match(
            value =>
                Ok(MapAuthenticationResponse(value)),
            Problem
        );
    }

    private static AuthenticationResponse MapAuthenticationResponse(AuthenticationResult authenticationResult)
    {
        return new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token
        );
    }
}