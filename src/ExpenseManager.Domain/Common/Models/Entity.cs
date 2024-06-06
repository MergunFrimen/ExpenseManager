namespace ExpenseManager.Domain.Common.Models;

public abstract class Entity : IEquatable<Entity>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid Id { get; }

    public bool Equals(Entity? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}