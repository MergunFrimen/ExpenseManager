using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ExpenseManagerDbContext dbContext) : ICategoryRepository
{
    public async Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbContext.Categories.AnyAsync(predicate, cancellationToken);
    }

    public async Task<List<Category>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .Where(category => category.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken) is not { } category)
            return Errors.Category.NotFound;

        return category;
    }

    public async Task<List<Category>> FindAsync(Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Categories.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        var newCategory = await dbContext.Categories.AddAsync(category, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return newCategory.Entity;
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var updatedCategory = dbContext.Categories.Update(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return updatedCategory.Entity;
    }

    public async Task<Category> RemoveAsync(Category transaction, CancellationToken cancellationToken)
    {
        dbContext.Categories.Remove(transaction);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task<List<Category>> RemoveRangeAsync(List<Category> transactions,
        CancellationToken cancellationToken)
    {
        dbContext.Categories.RemoveRange(transactions);

        await dbContext.SaveChangesAsync(cancellationToken);

        return transactions;
    }
}