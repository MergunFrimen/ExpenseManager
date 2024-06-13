using ExpenseManager.Presentation.Contracts.Export;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("v1/users/{userId}/transactions/export")]
public class ExportController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> ExportAll(ExportRequest request, Guid userId)
    {
        throw new NotImplementedException();
    }
}