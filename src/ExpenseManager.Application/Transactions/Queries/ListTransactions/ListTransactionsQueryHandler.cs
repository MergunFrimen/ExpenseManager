using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries.ListTransactions;

public class ListTransactionsQueryHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : IQueryHandler<ListTransactionsQuery, List<TransactionResult>>

{
    public async Task<ErrorOr<List<TransactionResult>>> Handle(ListTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetAllAsync(query.UserId, cancellationToken);

        return transactions.Match(
            value => value.Select(transaction => new TransactionResult(transaction)).ToList(),
            ErrorOr<List<TransactionResult>>.From
        );
    }
}