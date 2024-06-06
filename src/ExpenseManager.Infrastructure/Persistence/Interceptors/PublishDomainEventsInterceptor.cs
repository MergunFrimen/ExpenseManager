using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpenseManager.Infrastructure.Persistence.Interceptors;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
}