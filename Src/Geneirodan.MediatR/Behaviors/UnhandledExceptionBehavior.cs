using Ardalis.Result;
using Microsoft.Extensions.Logging;

namespace Geneirodan.MediatR.Behaviors;

/// <summary>
/// A pipeline behavior that catches and logs any unhandled exceptions thrown during request processing.
/// This behavior ensures that any unhandled exceptions are logged before being rethrown to be handled elsewhere in the application.
/// </summary>
/// <typeparam name="TRequest">
/// The type of the request being processed. This should be a non-nullable type.
/// </typeparam>
/// <typeparam name="TResponse">
/// The type of the response that will be returned after handling the request. This is typically a <see cref="Result"/> type.
/// </typeparam>
public sealed partial class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            LogError(ex, typeof(TRequest).Name);
            throw;
        }
    }
    
    [LoggerMessage(LogLevel.Error, "Request: Unhandled Exception for Request {RequestName}")]
    partial void LogError(Exception ex, string requestName);
}
