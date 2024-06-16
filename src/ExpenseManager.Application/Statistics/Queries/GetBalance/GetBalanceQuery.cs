using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetBalance;

public sealed record GetBalanceQuery(
    Guid UserId
) : IQuery<GetBalanceResult>;