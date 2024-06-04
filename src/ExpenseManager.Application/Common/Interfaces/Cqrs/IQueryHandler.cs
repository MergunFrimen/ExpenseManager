using ErrorOr;
using MediatR;

namespace ExpenseManager.Application.Common.Interfaces.Cqrs;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>>
    where TRequest : IQuery<TResponse>
{
}