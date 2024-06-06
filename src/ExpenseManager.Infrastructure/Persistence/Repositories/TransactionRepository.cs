using ExpenseManager.Application.Common.Interfaces.Persistence;
using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private static readonly List<Transaction> Transactions = [];

    public Transaction Add(Transaction transaction)
    {
        Transactions.Add(transaction);
        return transaction;
    }

    public Transaction? Remove(Guid transactionId)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == transactionId);

        if (transaction is not null)
            Transactions.Remove(transaction);

        return transaction;
    }

    public Transaction? Update(Transaction transaction)
    {
        var existingTransaction = Transactions.FirstOrDefault(t => t.Id == transaction.Id);

        if (existingTransaction is null) return existingTransaction;

        Transactions.Remove(existingTransaction);
        Transactions.Add(transaction);

        return existingTransaction;
    }

    public Transaction? GetById(Guid transactionId)
    {
        return Transactions.FirstOrDefault(t => t.Id == transactionId);
    }

    public List<Transaction> GetAllByUserId(Guid userId)
    {
        return Transactions.Where(t => t.UserId == userId).ToList();
    }
}