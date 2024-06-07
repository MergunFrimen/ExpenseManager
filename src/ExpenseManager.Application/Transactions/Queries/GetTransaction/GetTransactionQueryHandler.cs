using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Transactions.Queries.GetTransaction;

public class GetTransactionQueryHandler(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
    : IQueryHandler<GetTransactionQuery, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(GetTransactionQuery query,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetByIdAsync(query.Id, cancellationToken);
        if (transaction is null)
            return Errors.Transaction.TransactionNotFound;
        
        var category = await categoryRepository.GetByIdAsync(transaction.CategoryId, cancellationToken);
        if (category is null)
            return Errors.Category.CategoryNotFound;

        return new TransactionResult(transaction, category.Name);
    }
}