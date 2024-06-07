using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class CategoryRepository(ExpenseManagerDbContext dbContext) : ICategoryRepository
{
    private static List<Category> _categories = [];

    public Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}