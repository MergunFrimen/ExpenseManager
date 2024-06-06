using ExpenseManager.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpenseManager.Infrastructure.Persistence.Interceptors;

public class PublishDomainEventsInterceptor(IPublisher mediatr) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // TODO: enforce async
        throw new NotImplementedException();
        
        // PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        // return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
            return;

        var domainEntities = dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .Select(entityEntry => entityEntry.Entity)
            .ToList();
        
        var domainEvents = domainEntities
            .SelectMany(entity => entity.DomainEvents)
            .ToList();
        
        domainEntities.ForEach(
            entity => entity.ClearDomainEvents()
        );
        
        foreach (var domainEvent in domainEvents)
        {
            await mediatr.Publish(domainEvent);
        }
    }
}