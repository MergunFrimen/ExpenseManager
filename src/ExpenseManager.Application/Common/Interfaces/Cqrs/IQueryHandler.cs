using MediatR;
using ErrorOr;

namespace ExpenseManager.Application.Common.Interfaces.Cqrs;

public interface IQueryHandler<in TRequest, TResponse>: IRequestHandler<TRequest, ErrorOr<TResponse>>
    where TRequest : IQuery<TResponse>
{
    
}