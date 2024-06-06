using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    Transaction Add(Transaction transaction);
    Transaction? Remove(Guid transactionId);
    Transaction? Update(Transaction transaction);
    Transaction? GetById(Guid transactionId);
    List<Transaction> GetAllByUserId(Guid userId);
}