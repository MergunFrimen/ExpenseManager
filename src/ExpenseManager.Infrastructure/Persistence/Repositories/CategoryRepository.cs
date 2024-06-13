using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ExpenseManagerDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        var newCategory = await dbContext.Categories.AddAsync(category, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return newCategory.Entity;
    }

    public async Task<ErrorOr<Category>> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await dbContext.Categories.FindAsync(c => c.Id == id, cancellationToken) is not { } category)
            return Errors.Category.NotFound;
                
        dbContext.Categories.Remove(category);

        await dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ErrorOr<Category>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return category;
    }

    public async Task<ErrorOr<Category>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

        return category;
    }

    public async Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var categories = await dbContext.Categories.Where(c => c.UserId == userId).ToListAsync(cancellationToken);

        return categories;
    }
}