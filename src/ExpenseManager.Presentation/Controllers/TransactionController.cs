using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransactions;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Queries.GetTransaction;
using ExpenseManager.Application.Transactions.Queries.ListTransactions;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Presentation.Contracts.Transactions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("api/v{v:apiVersion}/transactions")]
public class TransactionController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var command = mapper.Map<CreateTransactionCommand>((request, userId.Value));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Created($"api/v1/categories/{value.Transaction.Id}", mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTransactionRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);
    
        var command = mapper.Map<UpdateTransactionCommand>((request, id, userId.Value));
        var result = await mediatr.Send(command);
    
        return result.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveTransactionsRequest request)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);
    
        var command = mapper.Map<RemoveTransactionsCommand>((request, userId.Value));
        var result = await mediatr.Send(command);
    
        return result.Match(
            value => Ok(mapper.Map<List<TransactionResponse>>(value)),
            Problem
        );
    }
    //
    // [HttpGet]
    // public async Task<IActionResult> List()
    // {
    //     var userId = GetUserId();
    //     if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
    //         return Problem(statusCode: StatusCodes.Status401Unauthorized,
    //             title: userId.FirstError.Description);
    //
    //     var query = new ListTransactionsQuery(userId.Value);
    //     var result = await mediatr.Send(query);
    //
    //     return result.Match(
    //         value => Ok(mapper.Map<List<TransactionResponse>>(value)),
    //         Problem
    //     );
    // }
    //
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);
    
        var query = new GetTransactionQuery(id, userId.Value);
        var result = await mediatr.Send(query);
    
        return result.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
}

