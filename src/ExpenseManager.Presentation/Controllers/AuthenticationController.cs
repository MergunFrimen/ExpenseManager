using ExpenseManager.Application.Authentication.Commands.Register;
using ExpenseManager.Application.Authentication.Queries.Login;
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

        return authenticationResult.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem
        );
    }
}