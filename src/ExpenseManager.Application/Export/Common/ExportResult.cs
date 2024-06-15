using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Export.Common;

public sealed record ExportResult(
    List<Transaction> Transactions,
    List<Category> Categories
);