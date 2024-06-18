using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ExpenseManagerDbContext dbContext) : ICategoryRepository
{
    public async Task<ErrorOr<bool>> ExistsAsync(Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Categories.AnyAsync(predicate, cancellationToken);
    }

    public async Task<ErrorOr<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken) is not { } category)
            return Errors.Category.NotFound;

        return category;
    }

    public async Task<ErrorOr<List<Category>>> FindAsync(Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await dbContext.Categories.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Category>> AddAsync(Category category, CancellationToken cancellationToken)
    {
        var newCategory = await dbContext.Categories.AddAsync(category, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return newCategory.Entity;
    }

    public async Task<ErrorOr<Category>> UpdateAsync(Category update, CancellationToken cancellationToken)
    {
        if (await dbContext.Categories.FirstOrDefaultAsync(c => c.User.Id == update.User.Id && c.Id == update.Id,
                cancellationToken) is not { } category)
            return Errors.Category.NotFound;

        category.Update(update);
        
        dbContext.Update(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ErrorOr<Category>> RemoveAsync(Category category, CancellationToken cancellationToken)
    {
        dbContext.Categories.Remove(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ErrorOr<List<Category>>> RemoveRangeAsync(List<Category> categories,
        CancellationToken cancellationToken)
    {
        dbContext.Categories.RemoveRange(categories);

        await dbContext.SaveChangesAsync(cancellationToken);

        return categories;
    }
}