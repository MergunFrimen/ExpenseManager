using ExpenseManager.Domain.Common.Models;

namespace ExpenseManager.Domain.Transactions.Events;

public sealed record TransactionCreatedEvent(Transaction Transaction) : IDomainEvent;