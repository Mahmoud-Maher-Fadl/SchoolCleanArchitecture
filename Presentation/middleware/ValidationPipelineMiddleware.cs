using Domain.common;
using FluentValidation;
using MediatR;

namespace Erp.middleware;

public class ValidationPipelineMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineMiddleware(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0) return await next();

        var errors = failures.Select(f => f.ErrorMessage).ToArray();

        return CreateValidationResult<TResponse>(errors);
    }

    private static TResponse CreateValidationResult<TResponse>(string[] errors)
        where TResponse : Result
    {
        var result = typeof(Result)
            .GetMethod(nameof(Result.Failure))!
            .MakeGenericMethod(typeof(TResponse).GenericTypeArguments[0])
            .Invoke(null, new object?[] { errors })!;
        return (TResponse)result;
    }
}