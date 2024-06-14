using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetBalance;

public class GetBalanceQueryHandler(
    ITransactionRepository transactionRepository)
    : IQueryHandler<GetBalanceQuery, BalanceResult>
{
    public async Task<ErrorOr<BalanceResult>> Handle(GetBalanceQuery query, CancellationToken cancellationToken)
    {
        var transactions =
            await transactionRepository.FindAsync(
                transaction => transaction.UserId == query.UserId &&
                               transaction.Date >= query.From &&
                               transaction.Date <= query.To
                , cancellationToken);

        if (!transactions.IsError)
            return ErrorOr<BalanceResult>.From(transactions.Errors);

        var totalBalance = transactions.Value
            .Select(transaction => transaction.Amount)
            .Sum();

        return new BalanceResult(totalBalance);
    }
}