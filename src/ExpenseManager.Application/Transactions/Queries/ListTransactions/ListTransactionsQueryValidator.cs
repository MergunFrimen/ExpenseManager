using ExpenseManager.Application.Transactions.Queries.GetTransaction;
using FluentValidation;

namespace ExpenseManager.Application.Transactions.Queries.ListTransactions;

public class ListTransactionsQueryValidator : AbstractValidator<GetTransactionQuery>
{
    public ListTransactionsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}