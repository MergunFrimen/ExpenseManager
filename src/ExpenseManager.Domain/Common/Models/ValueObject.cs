namespace ExpenseManager.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }
}