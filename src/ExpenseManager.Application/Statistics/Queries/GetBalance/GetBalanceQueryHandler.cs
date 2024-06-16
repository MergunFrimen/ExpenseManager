using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Statistics.Common;

namespace ExpenseManager.Application.Statistics.Queries.GetBalance;

public class GetBalanceQueryHandler(
    ITransactionRepository transactionRepository)
    : IQueryHandler<GetBalanceQuery, GetBalanceResult>
{
    public async Task<ErrorOr<GetBalanceResult>> Handle(GetBalanceQuery query, CancellationToken cancellationToken)
    {
        var transactions =
            await transactionRepository.FindAsync(transaction => transaction.User.Id == query.UserId,
                cancellationToken);
        if (transactions.IsError)
            return transactions.Errors;

        var totalBalance = transactions.Value
            .Select(transaction => transaction.Amount)
            .Sum();

        return new GetBalanceResult(totalBalance);
    }
}