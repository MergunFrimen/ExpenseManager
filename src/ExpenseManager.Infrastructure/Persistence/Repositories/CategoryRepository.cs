using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;
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

    public async Task<Category?> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);

        if (category is null) return null;
        
        dbContext.Categories.Remove(category);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return category;
    }
    
    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        
        return category;
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name, cancellationToken: cancellationToken);
        
        return category;
    }

    public async Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        var categories = await dbContext.Categories.Where(c => c.UserId == userId).ToListAsync(cancellationToken);
        
        return categories;
    }
}