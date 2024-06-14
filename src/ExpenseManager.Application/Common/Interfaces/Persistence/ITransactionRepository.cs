using System.Linq.Expressions;
using ErrorOr;
using ExpenseManager.Domain.Transactions;

namespace ExpenseManager.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository: IRepository<Transaction>
{
}