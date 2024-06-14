using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository)
    : ICommandHandler<UpdateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(UpdateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (transaction.IsError)
            return transaction.Errors;

        if (transaction.Value.UserId != command.UserId)
            return Errors.Transaction.Unauthorized;

        List<Category> categories = [];
        
        foreach (var categoryId in command.CategoryIds)
        {
            var category = await categoryRepository.GetByIdAsync(categoryId, cancellationToken);
            if (category.IsError)
                return category.Errors;

            categories.Add(category.Value);
        }
        
        var newTransaction = Transaction.Create(
            command.Id,
            command.UserId,
            command.Type,
            command.Description,
            command.Amount,
            command.Date,
            categories
        );

        await transactionRepository.RemoveAsync(command.Id, cancellationToken);
        await transactionRepository.AddAsync(newTransaction, cancellationToken);

        return new TransactionResult(newTransaction);
    }
}