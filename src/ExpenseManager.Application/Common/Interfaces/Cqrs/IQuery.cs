using ErrorOr;
using MediatR;

namespace ExpenseManager.Application.Common.Interfaces.Cqrs;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}