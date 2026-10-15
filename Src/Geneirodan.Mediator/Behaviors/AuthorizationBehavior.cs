using System.Reflection;
using Geneirodan.Abstractions.Domain;
using Geneirodan.Mediator.Attributes;

namespace Geneirodan.Mediator.Behaviors;

/// <summary>
/// A pipeline behavior that handles authorization logic for incoming requests.
/// It checks if the request has associated <see cref="AuthorizeAttribute"/> attributes, and ensures that the user
/// is authorized based on the roles defined in those attributes. If authorization fails, it returns a forbidden result.
/// </summary>
/// <typeparam name="TRequest">
/// The type of the request being handled. This should be a non-nullable type.
/// </typeparam>
/// <typeparam name="TResponse">
/// The type of the response that will be returned after handling the request. This type must be a <see cref="Result"/>.
/// </typeparam>
public sealed class AuthorizationBehavior<TRequest, TResponse>(IUser user) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
    where TResponse : class, IResult
{
    /// <inheritdoc/>
    public async ValueTask<TResponse> Handle(
        TRequest message,
        MessageHandlerDelegate<TRequest, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var authorizeAttributes = message.GetType().GetCustomAttributes<AuthorizeAttribute>().ToArray();

        if (authorizeAttributes.Length == 0)
            return await next(message, cancellationToken).ConfigureAwait(false);

        if (user.Id == Guid.Empty)
            return DynamicResults.Unauthorized<TResponse>();

        var authorizeAttributesWithRoles = authorizeAttributes
            .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
            .ToArray();

        if (authorizeAttributesWithRoles.Length == 0)
            return await next(message, cancellationToken).ConfigureAwait(false);

        var authorized = authorizeAttributesWithRoles
            .SelectMany(a => a.Roles.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Any(user.IsInRole);

        return authorized
            ? await next(message, cancellationToken).ConfigureAwait(false)
            : DynamicResults.Forbidden<TResponse>();
    }
}
