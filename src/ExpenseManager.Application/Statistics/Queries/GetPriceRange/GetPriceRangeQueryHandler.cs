using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetPriceRange;

public class GetPriceRangeQueryHandler(
    ITransactionRepository transactionRepository)
    : IQueryHandler<GetPriceRangeQuery, GetPriceRangeResult>
{
    public async Task<ErrorOr<GetPriceRangeResult>> Handle(GetPriceRangeQuery query,
        CancellationToken cancellationToken)
    {
        var transactions =
            await transactionRepository.FindAsync(transaction => transaction.User.Id == query.UserId,
                cancellationToken);
        if (transactions.IsError)
            return transactions.Errors;

        var minPrice = transactions.Value
            .Min(transaction => transaction.Amount);
        var maxPrice = transactions.Value
            .Max(transaction => transaction.Amount);

        return new GetPriceRangeResult(minPrice, maxPrice);
    }
}