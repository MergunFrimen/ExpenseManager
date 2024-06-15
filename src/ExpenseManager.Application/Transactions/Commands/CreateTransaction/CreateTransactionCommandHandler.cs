using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Common.Errors;
using ExpenseManager.Domain.Transactions;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;
        
        // Check if categories exist
        var categories = await categoryRepository.FindAsync(
            category => command.CategoryIds.Contains(category.Id), cancellationToken);
        if (categories.IsError)
            return categories.Errors;
        if (categories.Value.Count != command.CategoryIds.Length)
            return Errors.Category.NotFound;
        
        // Create transaction
        var transaction = Transaction.Create(
            null,
            command.Description,
            command.Amount,
            user.Value,
            command.Date,
            categories.Value
        );

        // Add transaction to the database
        var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);
        if (createdTransaction.IsError)
            return createdTransaction.Errors;

        return new TransactionResult(createdTransaction.Value);
    }
}

