using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;
using ExpenseManager.Domain.Users;

namespace ExpenseManager.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    IUserRepository userRepository)
    : ICommandHandler<CreateTransactionCommand, TransactionResult>
{
    public async Task<ErrorOr<TransactionResult>> Handle(CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user.IsError)
            return user.Errors;
        
        // check if the categories get created
        var transaction = Transaction.Create(
            null,
            command.Description,
            command.Amount,
            user.Value,
            CreateDefaultCategories(user.Value)
        );
        
        var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);
        if (createdTransaction.IsError)
            return createdTransaction.Errors;
        
        return new TransactionResult(createdTransaction.Value);
    }

    private static List<Category> CreateDefaultCategories(User user)
    {
        return [
            Category.Create(null, "Food", user),
            Category.Create(null, "Food", user),
            Category.Create(null, "Food", user),
        ];
    }
}

