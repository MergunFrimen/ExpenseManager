using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Domain.Users.Events;

public record TransactionCreated(Transaction Transaction) : IDomainEvent;