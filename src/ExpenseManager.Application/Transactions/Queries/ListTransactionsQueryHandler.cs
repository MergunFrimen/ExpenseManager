using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries;

public class ListTransactionsQueryHandler(ITransactionRepository transactionRepository)
    : IQueryHandler<ListTransactionsQuery, List<TransactionResult>>
{
    public async Task<ErrorOr<List<TransactionResult>>> Handle(ListTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetAllByUserIdAsync(query.UserId, cancellationToken);

        return new List<TransactionResult>(transactions.Select(transaction => new TransactionResult(transaction)));
    }
}