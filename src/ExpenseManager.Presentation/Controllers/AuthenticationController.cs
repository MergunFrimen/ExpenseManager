using Asp.Versioning;
using ExpenseManager.Application.Authentication.Commands.Register;
using ExpenseManager.Application.Authentication.Queries.Login;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Presentation.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[AllowAnonymous]
[Route("api/v{v:apiVersion}/auth")]
public class AuthenticationController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = mapper.Map<RegisterCommand>(request);
        var authenticationResult = await mediatr.Send(command);
        
        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.User.DuplicateEmail)
            return Problem(statusCode: StatusCodes.Status409Conflict,
                title: authenticationResult.FirstError.Description);

        return authenticationResult.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);
        var authenticationResult = await mediatr.Send(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);

        return authenticationResult.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem
        );
    }
}