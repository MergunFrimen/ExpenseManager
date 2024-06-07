using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransaction;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Queries;
using ExpenseManager.Presentation.Contracts.Transactions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("/transactions")]
public class TransactionController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<CreateTransactionCommand>((request, userId));
        var createTransactionResult = await mediatr.Send(command);

        return createTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTransactionRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<UpdateTransactionCommand>((request, userId));
        var updateTransactionResult = await mediatr.Send(command);

        return updateTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveTransactionRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<RemoveTransactionCommand>((request, userId));
        var removeTransactionResult = await mediatr.Send(command);

        return removeTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
    
    [HttpGet]
    public async Task<IActionResult> List(ListTransactionsRequest request)
    {
        var userId = GetUserId();
        var query = mapper.Map<ListTransactionsQuery>((request, userId));
        var getTransactionsResult = await mediatr.Send(query);

        return getTransactionsResult.Match(
            value => Ok(mapper.Map<List<TransactionResponse>>(value)),
            Problem
        );
    }
}