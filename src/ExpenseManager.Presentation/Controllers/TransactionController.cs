using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransaction;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Queries.GetTransaction;
using ExpenseManager.Application.Transactions.Queries.ListTransactions;
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
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTransactionRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<UpdateTransactionCommand>((request, userId));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveTransactionRequest request)
    {
        var userId = GetUserId();
        var command = mapper.Map<RemoveTransactionCommand>((request, userId));
        var result = await mediatr.Send(command);

        return result.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var userId = GetUserId();
        var query = new ListTransactionsQuery(userId);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<List<TransactionResponse>>(value)),
            Problem
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = GetUserId();
        var query = new GetTransactionQuery(id, userId);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
}