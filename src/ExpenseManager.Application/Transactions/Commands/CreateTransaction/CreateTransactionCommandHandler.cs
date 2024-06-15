using ErrorOr;
using ExpenseManager.Application.Common.Interfaces.Cqrs;
using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Application.Transactions.Common;
using ExpenseManager.Domain.Categories;
using ExpenseManager.Domain.Transactions;

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
            CreateDefaultCategories(user.Value.Id)
        );
        
        var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);
        
        if (createdTransaction.IsError)
            return createdTransaction.Errors;
        
        return new TransactionResult(createdTransaction.Value);
    }
    
    public static List<Category> CreateDefaultCategories(Guid userId)
    {
        return [
            Category.Create(null, "Food", userId),
            Category.Create(null, "Food", userId),
            Category.Create(null, "Food", userId),
        ];
    }
}

