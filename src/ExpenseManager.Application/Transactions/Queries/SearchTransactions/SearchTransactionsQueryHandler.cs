using ErrorOr;
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
        var allTransactions = await transactionRepository.FindAsync(category => true, cancellationToken);
        if (allTransactions.IsError)
            return allTransactions.Errors;

        var result = allTransactions.Value;

        if (query.Filters.Description is not null)
            result = result
                .Where(transaction => transaction.Description.ToLower().Contains(query.Filters.Description.ToLower()))
                .ToList();

        if (query.Filters.TransactionType is not null)
            result = result
                .Where(transaction => transaction.Type == query.Filters.TransactionType)
                .ToList();

        if (query.Filters.CategoryIds is not null)
            result = result
                .Where(transaction => transaction.Categories.Select(category => category.Id)
                    .Intersect(query.Filters.CategoryIds)
                    .Any())
                .ToList();

        if (query.Filters.DateRange is not null)
        {
            var startDate = query.Filters.DateRange.StartDate ?? 0;
            var endDate = query.Filters.DateRange.EndDate ?? ulong.MaxValue;
            result = result
                .Where(transaction => transaction.Date >= startDate && transaction.Date <= endDate)
                .ToList();
        }


        return result.Select(transaction => new TransactionResult(transaction)).ToList();
    }
}