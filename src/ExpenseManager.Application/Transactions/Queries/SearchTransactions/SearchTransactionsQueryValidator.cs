using ExpenseManager.Application.Categories.Queries.SearchCategories;
using ExpenseManager.Application.Common.Extensions;
using ExpenseManager.Domain.Transactions;
using FluentValidation;

namespace ExpenseManager.Application.Transactions.Queries.SearchTransactions;

public class SearchCategoriesQueryValidator : AbstractValidator<SearchTransactionsQuery>
{
    public SearchCategoriesQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(Transaction.DescriptionMaxLength);
    }
}