using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Common.Errors;

namespace ExpenseManager.Application.Transactions.Queries;

public class ListTransactionsQueryHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : IQueryHandler<ListTransactionsQuery, List<TransactionResult>>
{
    public async Task<ErrorOr<List<TransactionResult>>> Handle(ListTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetAllAsync(query.UserId, cancellationToken);
        List<TransactionResult> result = [];

        foreach (var transaction in transactions)
        {
            var category = await categoryRepository.GetByIdAsync(transaction.CategoryId, cancellationToken);
            
            if (category is null)
                return Errors.Category.CategoryNotFound;

            result.Add(new TransactionResult(transaction, category!.Name));
        }

        return result;
    }
}