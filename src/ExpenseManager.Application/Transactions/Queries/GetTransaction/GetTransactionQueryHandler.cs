using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Transactions.Queries.GetTransaction;

public class GetTransactionQueryHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : IQueryHandler<GetTransactionQuery, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(GetTransactionQuery query,
        CancellationToken cancellationToken)
    {
        List<Error> errors = [];
        
        var transaction = await transactionRepository.GetByIdAsync(query.Id, cancellationToken);
        if (transaction.IsError)
        {
            errors.AddRange(transaction.Errors);
            return errors;
        }

        var category = await categoryRepository.GetByIdAsync(transaction.Value.CategoryId, cancellationToken);
        if (category.IsError)
        {
            errors.AddRange(category.Errors);
            return errors;

        }
        
        return new TransactionResult(transaction.Value, category.Value.Name);
    }
}