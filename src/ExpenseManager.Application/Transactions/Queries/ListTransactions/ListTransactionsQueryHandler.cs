using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

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
            onValue: value => value.Select(transaction => new TransactionResult(transaction)).ToList(),
            onError: ErrorOr<List<TransactionResult>>.From
        );
    }
}