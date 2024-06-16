using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Domain.Transactions.ValueObjects;
using ExpenseManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Services;

public class ChartsService(ExpenseManagerDbContext dbContext) : IChartsService
{
    public async Task<List<CategoryTotal>> CalculateCategoryDonutChart(Guid userId, TransactionType type,
        CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions
            .Where(c => c.User.Id == userId && c.Type == type)
            .Include(transaction => transaction.Categories)
            .ToListAsync(cancellationToken);

        var categories = new Dictionary<string, decimal>();

        foreach (var transaction in transactions)
        foreach (var category in transaction.Categories)
            if (categories.ContainsKey(category.Name))
                categories[category.Name] += transaction.Amount;
            else
                categories.Add(category.Name, transaction.Amount);

        return categories.Select(category => new CategoryTotal(category.Key, category.Value)).ToList();
    }
}