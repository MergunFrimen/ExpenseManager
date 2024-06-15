// using ErrorOr;
// using ExpenseManager.Application.Categories.Commands.UpdateTransaction;
// using ExpenseManager.Application.Categories.Common;
// using ExpenseManager.Application.Common.Interfaces.Cqrs;
// using ExpenseManager.Application.Common.Interfaces.Persistence;
// using ExpenseManager.Application.Transactions.Common;
// using ExpenseManager.Domain.Categories;
// using ExpenseManager.Domain.Common.Errors;
// using ExpenseManager.Domain.Transactions;
//
// namespace ExpenseManager.Application.Transactions.Commands.UpdateTransaction;
//
// public class UpdateTransactionCommandHandler(
//     ITransactionRepository transactionRepository)
//     : ICommandHandler<UpdateTransactionCommand, TransactionResult>
// {
//     public async Task<ErrorOr<TransactionResult>> Handle(UpdateTransactionCommand command,
//         CancellationToken cancellationToken)
//     {
//         // Check if the transaction exists
//         var exists = await transactionRepository.ExistsAsync(
//             transaction => transaction.Id == command.Id && transaction.UserId == command.UserId,
//             cancellationToken);
//         
//         if (exists.IsError)
//             return exists.Errors;
//         if (!exists.Value)
//             return Errors.Transaction.NotFound;
//         
//         var transaction = Transaction.Create(
//             command.Id,
//             command.UserId,
//             command.CategoryIds,
//             command.Type,
//             command.Description,
//             command.Amount,
//             command.Date
//         );
//         var updatedTransaction = await transactionRepository.UpdateAsync(transaction, cancellationToken);
//
//         return updatedTransaction.Match(
//             value => new TransactionResult(value),
//             ErrorOr<TransactionResult>.From
//         );
//     }
// }

