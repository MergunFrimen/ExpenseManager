using ExpenseManager.Domain.Common.Models;
using ExpenseManager.Domain.Users.Entities;

namespace ExpenseManager.Domain.Users.Events;

public sealed record TransactionCreatedEvent(Transaction Transaction) : IDomainEvent;