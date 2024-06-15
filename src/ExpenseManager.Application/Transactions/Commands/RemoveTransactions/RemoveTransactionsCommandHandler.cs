// using ErrorOr;
// using ExpenseManager.Application.Common.Interfaces.Cqrs;
// using ExpenseManager.Application.Common.Interfaces.Persistence;
// using ExpenseManager.Application.Transactions.Common;
//
// namespace ExpenseManager.Application.Transactions.Commands.RemoveTransactions;
//
// public class RemoveTransactionsCommandHandler(ITransactionRepository transactionRepository)
//     : ICommandHandler<RemoveTransactionsCommand, List<TransactionResult>>
// {
//     public async Task<ErrorOr<List<TransactionResult>>> Handle(RemoveTransactionsCommand command, CancellationToken cancellationToken)
//     {
//         var transactions = await transactionRepository.FindAsync(
//             transaction => command.TransactionIds.Contains(transaction.Id) && transaction.UserId == command.UserId,
//             cancellationToken);
//
//         if (transactions.IsError)
//             return transactions.Errors;
//         
//         var removedTransactions = await transactionRepository.RemoveRangeAsync(transactions.Value, cancellationToken);
//         
//         if (removedTransactions.IsError)
//             return removedTransactions.Errors;
//         
//         return removedTransactions.Value.Select(transaction => new TransactionResult(transaction)).ToList();
//     }
// }

