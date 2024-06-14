using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ExpenseManagerDbContext dbContext) : ICategoryRepository
{
    public async Task<ErrorOr<List<Category>>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
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

    public async Task<ErrorOr<Category>> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        dbContext.Categories.Update(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ErrorOr<Category>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = dbContext.Categories.FirstOrDefault(category => category.Id == id);
        if (category is null)
            return Errors.Category.NotFound;

        dbContext.Categories.Remove(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ErrorOr<List<Category>>> RemoveRangeAsync(List<Guid> id, CancellationToken cancellationToken)
    {
        var categories = dbContext.Categories.Where(category => id.Contains(category.Id)).ToList();

        await dbContext
            .Categories
            .Where(category => id.Contains(category.Id))
            .ExecuteDeleteAsync(cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return categories;
    }
}