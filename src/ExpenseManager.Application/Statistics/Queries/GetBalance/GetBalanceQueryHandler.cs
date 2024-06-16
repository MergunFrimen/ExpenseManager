using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;

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

=        var totalExpenses = transactions.Value
            .Where(transaction => transaction.Type == TransactionType.Expense)
            .Select(transaction => transaction.Amount)
            .Sum();
        var totalIncome = transactions.Value
            .Where(transaction => transaction.Type == TransactionType.Income)
            .Select(transaction => transaction.Amount)
            .Sum();
        var totalBalance = totalIncome - totalExpenses;

        return new GetBalanceResult(totalBalance, totalExpenses, totalIncome);
    }
}