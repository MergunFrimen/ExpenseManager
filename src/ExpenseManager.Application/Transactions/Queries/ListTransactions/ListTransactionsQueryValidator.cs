using FluentValidation;

namespace ExpenseManager.Application.Transactions.Queries.ListTransactions;

public class ListTransactionsQueryValidator : AbstractValidator<GetTransaction.GetTransactionQuery>
{
    public ListTransactionsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}