using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Application.Transactions.Queries.ListTransactions;

namespace ExpenseManager.Application.Statistics.Queries.GetBalance;

public class GetBalanceQueryHandler(
    ITransactionRepository transactionRepository)
    : IQueryHandler<GetBalanceQuery, BalanceResult>
{
    public async Task<ErrorOr<BalanceResult>> Handle(GetBalanceQuery query, CancellationToken cancellationToken)
    {
        var transactions =
            await transactionRepository.GetAllDateRangeAsync(query.UserId, query.From, query.To, cancellationToken);
        
        var balance = transactions
            .Select(transaction => transaction.Amount)
            .Sum();
        var totalExpenses = transactions
            .Where(transaction => transaction.Amount < 0)
            .Select(transaction => transaction.Amount)
            .Sum();
        var totalIncome = transactions
            .Where(transaction => transaction.Amount > 0)
            .Select(transaction => transaction.Amount)
            .Sum();
        
        return new BalanceResult(balance, totalExpenses, totalIncome);
    }
}