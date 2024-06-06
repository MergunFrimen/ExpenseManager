using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Application.Transactions.Commands.RemoveTransaction;
using ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
using ExpenseManager.Application.Transactions.Queries;
using ExpenseManager.Presentation.Contracts.Transactions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("users/{userId}/transactions")]
public class TransactionController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetTransactions(Guid userId)
    {
        var query = mapper.Map<ListTransactionsQuery>(userId);
        var getTransactionsResult = await mediatr.Send(query);

        return getTransactionsResult.Match(
            value => Ok(mapper.Map<List<TransactionResponse>>(value)),
            Problem
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request, Guid userId)
    {
        var command = mapper.Map<CreateTransactionCommand>((request, userId));
        var createTransactionResult = await mediatr.Send(command);

        return createTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpPut("{transactionId}")]
    public async Task<IActionResult> UpdateTransaction(UpdateTransactionRequest request, Guid userId,
        Guid transactionId)
    {
        var command = mapper.Map<UpdateTransactionCommand>((request, userId, transactionId));
        var updateTransactionResult = await mediatr.Send(command);

        return updateTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }

    [HttpDelete("{transactionId}")]
    public async Task<IActionResult> RemoveTransaction(Guid userId, Guid transactionId)
    {
        var command = mapper.Map<RemoveTransactionCommand>((userId, transactionId));
        var removeTransactionResult = await mediatr.Send(command);

        return removeTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
}