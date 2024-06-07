using ExpenseManager.Domain.Transactions.Events;
using MediatR;

namespace ExpenseManager.Application.Transactions.Events;

public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
{
    public Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}