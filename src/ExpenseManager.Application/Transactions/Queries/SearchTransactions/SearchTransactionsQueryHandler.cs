using ErrorOr;
using ExpenseManager.Application.Categories.Common;
using ExpenseManager.Application.Categories.Queries.SearchCategories;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries.SearchTransactions;

public class SearchTransactionsQueryHandler(ITransactionRepository transactionRepository)
    : IQueryHandler<SearchTransactionsQuery, List<TransactionResult>>
{
    public async Task<ErrorOr<List<TransactionResult>>> Handle(SearchTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.FindAsync(
            category =>
                category.User.Id == query.UserId &&
                category.Description.ToLower().Contains(query.Description.ToLower()),
            cancellationToken);

        return transactions.Match(
            value => value.Select(transaction => new TransactionResult(transaction)).ToList(),
            ErrorOr<List<TransactionResult>>.From
        );
    }
}