using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace Geneirodan.MediatR.Behaviors;

/// <summary>
/// A pipeline behavior that performs request validation before passing it to the handler.
/// This behavior validates the request using a collection of validators and returns an invalid result
/// if any validation errors are found. Otherwise, it proceeds to the next step in the pipeline.
/// </summary>
/// <typeparam name="TRequest">
/// The type of the request being processed. It is a non-nullable type that represents the request.
/// </typeparam>
/// <typeparam name="TResponse">
/// The type of the response returned by the request handler. It must be a result type that can encapsulate validation errors.
/// </typeparam>
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, IResult
{
    /// <inheritdoc />
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (!validators.Any())
            return await next(cancellationToken).ConfigureAwait(false);

        var context = new ValidationContext<TRequest>(request);
        var validationTasks = validators.Select(v => v.ValidateAsync(context, cancellationToken));
        var validationResults = await Task.WhenAll(validationTasks).ConfigureAwait(false);

        if (validationResults.All(x => x.IsValid))
            return await next(cancellationToken).ConfigureAwait(false);

        var errors = validationResults.SelectMany(x => x.AsErrors()).ToArray();

        return DynamicResults.Invalid<TResponse>(errors);
    }
}