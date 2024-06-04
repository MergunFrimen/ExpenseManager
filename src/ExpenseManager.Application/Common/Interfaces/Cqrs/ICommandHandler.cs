using ErrorOr;
using MediatR;

namespace ExpenseManager.Application.Common.Interfaces.Cqrs;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>>
    where TRequest : ICommand<TResponse>
{
}