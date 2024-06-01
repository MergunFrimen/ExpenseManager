using ExpenseManager.Application.Common.Interfaces.Services;

namespace ExpenseManager.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}