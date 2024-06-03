using ErrorOr;
using FluentValidation;
using MediatR;

namespace ExpenseManager.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
        return (dynamic)errors;
    }
}

// public class CommandValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) :
//     IPipelineBehavior<TRequest, ErrorOr<TResponse>>
//     where TRequest : ICommand<TResponse>
// {
//     public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next, CancellationToken cancellationToken)
//     {
//         if (validator is null)
//         {
//             return await next();
//         }
//
//         var validationResult = await validator.ValidateAsync(request, cancellationToken);
//
//         if (!validationResult.IsValid)
//             return await next();
//
//         var errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(
//             validationFailure.PropertyName,
//             validationFailure.ErrorMessage));
//
//         return errors;
//     }
// }
//
// public class QueryValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) :
//     IPipelineBehavior<TRequest, ErrorOr<TResponse>>
//     where TRequest : IQuery<TResponse>
// {
//     public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next, CancellationToken cancellationToken)
//     {
//         if (validator is null)
//         {
//             return await next();
//         }
//
//         var validationResult = await validator.ValidateAsync(request, cancellationToken);
//
//         if (!validationResult.IsValid)
//             return await next();
//
//         var errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(
//             validationFailure.PropertyName,
//             validationFailure.ErrorMessage));
//
//         return errors;
//     }
// }
