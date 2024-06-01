using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManager.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var problemDetails = new ProblemDetails
        {
            Status = 500,
            Title = exception.Message,
            Type = "https://www.rfc-editor.org/rfc/rfc9110.html#section-15.6.1"
        };

        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}