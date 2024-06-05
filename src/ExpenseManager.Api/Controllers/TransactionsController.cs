using ExpenseManager.Application.Authentication.Commands.Register;
using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Contracts.Transactions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

[Route("users/{userId}/transactions")]
public class TransactionsController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request, string userId)
    {
        var command = mapper.Map<CreateTransactionCommand>(request);
        var result = await mediatr.Send(command);
        
        return Ok(request);
    }
}