using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}