using ExpenseManager.Application.Authentication.Commands.Register;
using ExpenseManager.Application.Authentication.Queries.Login;
using ExpenseManager.Application.Services.Authentication;
using ExpenseManager.Contracts.Authentication;
using ExpenseManager.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

[Route("auth")]
public class AuthenticationController(IMediator mediator) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        var authenticationResult = await mediator.Send(command);

        return authenticationResult.Match(
            value =>
                Ok(MapAuthenticationResponse(value)),
            Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password
        );
        var authenticationResult = await mediator.Send(query);

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