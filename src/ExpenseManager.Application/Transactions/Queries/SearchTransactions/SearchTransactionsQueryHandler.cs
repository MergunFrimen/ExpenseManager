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
        var allTransactions =
            await transactionRepository.FindAsync(category => category.User.Id == query.UserId, cancellationToken);
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
            var from = query.Filters.DateRange.From ?? ulong.MinValue;
            var to = query.Filters.DateRange.To ?? ulong.MaxValue;
            result = result
                .Where(transaction => from <= transaction.Date && transaction.Date <= to)
                .ToList();
        }

        if (query.Filters.PriceRange is not null)
        {
            var from = query.Filters.PriceRange.From ?? decimal.MinValue;
            var to = query.Filters.PriceRange.To ?? decimal.MaxValue;
            result = result
                .Where(transaction => from <= transaction.Amount && transaction.Amount <= to)
                .ToList();
        }

        return result.Select(transaction => new TransactionResult(transaction)).ToList();
    }
}