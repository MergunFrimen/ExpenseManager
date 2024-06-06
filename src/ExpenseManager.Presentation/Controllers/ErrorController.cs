using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}