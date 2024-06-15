using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;

namespace ExpenseManager.Application.Transactions.Queries.GetTransaction;

public class GetTransactionQueryHandler(
    ITransactionRepository transactionRepository)
    : IQueryHandler<GetTransactionQuery, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(GetTransactionQuery query,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetByIdAsync(query.Id, cancellationToken);

        return transaction.Match(
            value => new TransactionResult(value),
            ErrorOr<TransactionResult>.From
        );
    }
}