using ErrorOr;
using ExpenseManager.Api.Common.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,

            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}