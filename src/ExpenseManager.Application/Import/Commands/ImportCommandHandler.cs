using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Import.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Import.Commands;

public class ImportCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository)
    : ICommandHandler<ImportCommand, ImportResult>
{
    public async Task<ErrorOr<ImportResult>> Handle(ImportCommand command, CancellationToken cancellationToken)
    {
        var amountTransactionAdded = 0;
        var amountCategoryAdded = 0;
        
        // Get user
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;

        // Create categories and transactions
        foreach (var category in command.Categories)
        {
            var categoryEntity = Category.Create(category.Id, category.Name, user.Value);
            var createdCategory = await categoryRepository.AddAsync(categoryEntity, cancellationToken);
            if (createdCategory.IsError)
                return createdCategory.Errors;
            
            amountCategoryAdded++;
        }

        foreach (var transaction in command.Transactions)
        {
            var categoryIds = transaction.Categories.Select(category => category.Id).ToList();
            var categories = await categoryRepository.FindAsync(
                category => category.User.Id == command.UserId && categoryIds.Contains(category.Id), cancellationToken);
            if (categories.IsError)
                return categories.Errors;

            var transactionEntity = Transaction.Create(
                transaction.Id,
                transaction.Description,
                transaction.Amount,
                transaction.Type,
                user.Value,
                transaction.Date,
                categories.Value
            );

            var createdTransaction = await transactionRepository.AddAsync(transactionEntity, cancellationToken);
            if (createdTransaction.IsError)
                return createdTransaction.Errors;
            
            amountTransactionAdded++;
        }

        return new ImportResult(amountTransactionAdded, amountCategoryAdded);
    }
}