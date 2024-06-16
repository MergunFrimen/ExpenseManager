using ExpenseManager.Application.Export.Queries;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Presentation.Contracts.Export;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("api/v{v:apiVersion}/export")]
public class ExportController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> Export()
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = new ExportQuery(userId.Value);
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<ExportResponse>(value)),
            Problem
        );
    }
}