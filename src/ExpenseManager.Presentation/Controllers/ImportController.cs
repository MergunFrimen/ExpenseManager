using ExpenseManager.Application.Import.Commands;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Presentation.Contracts.Import;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("api/v{v:apiVersion}/import")]
public class ImportController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Import(ImportRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<ImportCommand>((request, userId.Value));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<ImportResponse>(value)),
            Problem
        );
    }
}