using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Import.Common;

public sealed record ImportResult(
    int AmountTransactionAdded,
    int AmountCategoryAdded
);