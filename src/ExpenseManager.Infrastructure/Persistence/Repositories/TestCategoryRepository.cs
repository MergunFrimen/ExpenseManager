using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TestCategoryRepository(ExpenseManagerDbContext dbContext) : ICategoryRepository
{
    private static readonly List<Category> Categories = [];

    public void TestConnection()
    {
        var connection = dbContext.Database.GetDbConnection();
        var canConnect = dbContext.Database.CanConnect();
        Console.WriteLine("Test connection");
    }

    public Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        TestConnection();
        
        Categories.Add(category);
        
        return Task.FromResult(category);
    }

    public Task<Category?> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = Categories.FirstOrDefault(c => c.Id == id);
        
        if (category is null) return Task.FromResult<Category?>(null);
        
        Categories.Remove(category);
        
        return Task.FromResult<Category?>(category);
    }
    
    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = Categories.FirstOrDefault(c => c.Id == id);
        
        return Task.FromResult(category);
    }

    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var category = Categories.FirstOrDefault(c => c.Name == name);
        
        return Task.FromResult(category);
    }

    public Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Task.FromResult(Categories);
    }
}