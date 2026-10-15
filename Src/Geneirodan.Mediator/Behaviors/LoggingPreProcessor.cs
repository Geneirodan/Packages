using Geneirodan.Abstractions.Domain;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Geneirodan.Mediator.Behaviors;

/// <summary>
/// A pipeline behavior that logs information about incoming requests before they are processed.
/// It logs the name of the request, the user ID, and the details of the request itself.
/// </summary>
/// <typeparam name="TRequest">
/// The type of the request being processed. This should be a non-nullable type.
/// </typeparam>
/// <typeparam name="TResponse">
/// The type of the response that will be returned after handling the request.
/// </typeparam>
public sealed partial class LoggingPreProcessor<TRequest, TResponse>(ILogger<TRequest> logger)
    : MessagePreProcessor<TRequest, TResponse>
    where TRequest : IMessage
{
    private readonly IUser? _user;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingPreProcessor{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger to use for logging.
    /// </param>
    /// <param name="user">
    /// The user associated with the request.
    /// </param>
    public LoggingPreProcessor(ILogger<TRequest> logger, IUser? user)
        : this(logger) => _user = user;

    [LoggerMessage(LogLevel.Information, "Processing {RequestName}")]
    partial void LogProcessingRequest(string requestName);

    [LoggerMessage(LogLevel.Information, "Processing {RequestName} with user {UserId}")]
    partial void LogProcessingRequestWithUserId(string requestName, Guid userId);

    /// <inheritdoc />
    protected override ValueTask Handle(TRequest message, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        using (LogContext.PushProperty("Request", message, destructureObjects: true))
            if (_user is not null && _user.Id != Guid.Empty)
                LogProcessingRequestWithUserId(requestName, _user.Id);
            else
                LogProcessingRequest(requestName);

        return ValueTask.CompletedTask;
    }
}
