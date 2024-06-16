using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Export.Common;

namespace ExpenseManager.Application.Export.Queries;

public class ExportQueryHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository)
    : IQueryHandler<ExportQuery, ExportResult>
{
    public async Task<ErrorOr<ExportResult>> Handle(ExportQuery query, CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await userRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;

        // Get transactions
        var transactions = await transactionRepository.FindAsync(
            transaction => transaction.User.Id == query.UserId, cancellationToken);
        if (transactions.IsError)
            return transactions.Errors;


        // Get categories
        var categories = await categoryRepository.FindAsync(
            category => category.User.Id == query.UserId, cancellationToken);
        if (categories.IsError)
            return categories.Errors;

        return new ExportResult(transactions.Value, categories.Value);
    }
}