using ExpenseManager.Application.Statistics.Queries.GetBalance;
using ExpenseManager.Application.Statistics.Queries.GetCharts;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Presentation.Contracts.Statistics;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManager.Presentation.Controllers;

[Route("api/v{v:apiVersion}/statistics")]
public class StatisticsController(ISender mediatr, IMapper mapper) : ApiController
{
    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance()
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var query = new GetBalanceQuery(userId.Value);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<GetBalanceResponse>(value)),
            Problem
        );
    }

    [HttpGet("charts")]
    public async Task<IActionResult> GetCharts()
    {
        var userId = GetUserId();
        if (userId.IsError && userId.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: userId.FirstError.Description);

        var query = new GetChartsQuery(userId.Value);
        var result = await mediatr.Send(query);

        return result.Match(
            value => Ok(mapper.Map<ChartsResponse>(value)),
            Problem
        );
    }
}