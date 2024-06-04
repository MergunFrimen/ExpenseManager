using System.Diagnostics;
using ErrorOr;
using ExpenseManager.Api.Common.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace ExpenseManager.Api.Common.Errors;

public class ExpenseManagerProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly Action<ProblemDetailsContext>? _configure;
    private readonly ApiBehaviorOptions _options;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExpenseManagerProblemDetailsFactory" /> class.
    /// </summary>
    /// <param name="options">The options for API behavior.</param>
    /// <param name="problemDetailsOptions">The options for customizing problem details.</param>
    public ExpenseManagerProblemDetailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    }


    /// <inheritdoc />
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <inheritdoc />
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode, Type = type, Detail = detail, Instance = instance
        };

        if (title != null)
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null) problemDetails.Extensions["traceId"] = traceId;

        _configure?.Invoke(new ProblemDetailsContext { HttpContext = httpContext!, ProblemDetails = problemDetails });

        if (httpContext?.Items[HttpContextItemKeys.Errors] is List<Error> errors)
            problemDetails.Extensions.Add("errorCodes", errors.Select(error => error.Code));
    }
}