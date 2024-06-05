using ExpenseManager.Application.Transactions.Commands.CreateTransaction;
using ExpenseManager.Contracts.Transactions;
using ExpenseManager.Domain.Ledger;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Api.Controllers;

[Route("users/{userId}/ledgers/{ledgerId}/transactions")]
public class LedgerController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request, string ledgerId)
    {
        var command = mapper.Map<CreateTransactionCommand>((request, ledgerId));
        var createTransactionResult = await mediatr.Send(command);

        return createTransactionResult.Match(
            value => Ok(mapper.Map<TransactionResponse>(value)),
            Problem
        );
    }
}