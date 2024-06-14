using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Application.Statistics.Common;
using ExpenseManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Infrastructure.Services;

public class ChartsService(ExpenseManagerDbContext dbContext) : IChartsService
{
    public async Task<ErrorOr<CategoryDonutChart>> CalculateCategoryDonutChart(Guid userId, DateTime from, DateTime to,
        CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions
            .Where(transaction => transaction.UserId == userId)
            .Where(transaction => transaction.Date >= from && transaction.Date <= to)
            .ToListAsync(cancellationToken);

        var expenses = transactions
            .Where(transaction => transaction.Amount < 0)
            .ToList();

        var incomes = transactions
            .Where(transaction => transaction.Amount > 0)
            .ToList();

        var categories = await dbContext.Categories
            .Where(category => category.UserId == userId)
            .ToListAsync(cancellationToken);

        var expenseTotals = categories
            .Select(category => new
            {
                Category = category,
                Total = expenses
                    .Where(transaction => transaction.CategoryIds.Contains(category.Id))
                    .Sum(transaction => transaction.Amount)
            })
            .Select(categoryTotal => (categoryTotal.Category, categoryTotal.Total))
            .ToList();

        var incomeTotals = categories
            .Select(category => new
            {
                Category = category,
                Total = incomes
                    .Where(transaction => transaction.CategoryIds.Contains(category.Id))
                    .Sum(transaction => transaction.Amount)
            })
            .Select(categoryTotal => (categoryTotal.Category, categoryTotal.Total))
            .ToList();


        return new CategoryDonutChart(
            new ExpenseCategoryDonutChart(
                expenseTotals.Sum(categoryTotal => categoryTotal.Total),
                expenseTotals.Select(categoryTotal =>
                    new ExpenseCategoryTotal(categoryTotal.Category.Name, categoryTotal.Total)).ToList()
            ),
            new IncomeCategoryDonutChart(
                incomeTotals.Sum(categoryTotal => categoryTotal.Total),
                incomeTotals.Select(categoryTotal =>
                    new IncomeCategoryTotal(categoryTotal.Category.Name, categoryTotal.Total)).ToList()
            )
        );
    }
}