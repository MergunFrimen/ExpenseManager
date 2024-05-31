using ExpenseManager.Api.Infrastructure;
using ExpenseManager.Application.Services.Authentication;
using ExpenseManager.Contracts.Authentication;

namespace ExpenseManager.Api.Endpoints;

public class Authentication(IAuthenticationService authenticationService) : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Register)
            .MapPost(Login);
    }
    
    public void Register(RegisterRequest request)
    {
        var result = authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var response = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token
        );

        return;
    }
    
    public void Login(LoginRequest request)
    {
        var result = authenticationService.Login(
            request.Email,
            request.Password
        );
        
        var response = new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token
        );

        return;
    }

}