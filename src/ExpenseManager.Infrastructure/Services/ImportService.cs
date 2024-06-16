using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Import.Commands;
using ExpenseManager.Application.Import.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;
using ExpenseManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Services;

public class ImportService(
    ExpenseManagerDbContext dbContext
) : IImportService
{
    public async Task<ErrorOr<ImportResult>> ImportAsync(ImportCommand command, CancellationToken cancellationToken)
    {
        var amountTransactionAdded = 0;
        var amountCategoryAdded = 0;

        // Get user
        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken) is not
            { } user)
            return Errors.User.NotFound;

        await using (var dbTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            // Delete all user transactions and categories

            var transactionsToDelete = await dbContext.Transactions
                .Where(transaction => transaction.User.Id == command.UserId)
                .ToListAsync(cancellationToken);
            dbContext.Transactions.RemoveRange(transactionsToDelete);

            var categoriesToDelete = await dbContext.Categories
                .Where(category => category.User.Id == command.UserId)
                .ToListAsync(cancellationToken);
            dbContext.Categories.RemoveRange(categoriesToDelete);

            await dbContext.SaveChangesAsync(cancellationToken);

            // Create categories and transactions

            foreach (var category in command.Categories)
            {
                // Skip duplicates
                if (await dbContext.Categories.FirstOrDefaultAsync(
                        c =>
                            c.User.Id == user.Id &&
                            c.Name == category.Name,
                        cancellationToken) is not null)
                    return Errors.Category.Duplicate;

                await dbContext.Categories.AddAsync(
                    Category.Create(
                        category.Id,
                        category.Name,
                        user
                    ), cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                amountCategoryAdded++;
            }

            foreach (var transaction in command.Transactions)
            {
                List<Category> categories = [];

                // Get all the categories of the transaction
                foreach (var transactionCategory in transaction.Categories)
                {
                    var existingCategory =
                        await dbContext.Categories.FirstOrDefaultAsync(
                            category =>
                                category.User.Id == user.Id &&
                                category.Id == transactionCategory.Id,
                            cancellationToken);

                    if (existingCategory is null)
                        return Errors.Category.NotFound;

                    categories.Add(existingCategory);
                }

                await dbContext.Transactions.AddAsync(Transaction.Create(
                    transaction.Id,
                    transaction.Description,
                    transaction.Amount,
                    transaction.Type,
                    user,
                    transaction.Date,
                    categories
                ), cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                amountTransactionAdded++;
            }

            await dbTransaction.CommitAsync(cancellationToken);
        }

        return new ImportResult(amountTransactionAdded, amountCategoryAdded);
    }
}