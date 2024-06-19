using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetPriceRange;

public sealed record GetPriceRangeQuery(
    Guid UserId
) : IQuery<GetPriceRangeResult>;