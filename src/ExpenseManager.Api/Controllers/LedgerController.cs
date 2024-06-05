using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Contracts.Transactions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

[Route("users/{userId}/transactions")]
public class LedgerController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request, string userId)
    {
        var command = mapper.Map<CreateTransactionCommand>((request, userId));
        var createTransactionResult = await mediatr.Send(command);

        return createTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
}