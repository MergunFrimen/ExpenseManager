using ErrorOr;
using MediatR;

namespace ExpenseManager.Application.Common.Interfaces.Cqrs;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}