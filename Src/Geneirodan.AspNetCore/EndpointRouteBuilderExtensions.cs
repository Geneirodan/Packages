using System.Diagnostics.CodeAnalysis;
using HealthChecks.UI.Client;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace Geneirodan.AspNetCore;

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/> to map health check endpoints
/// with a default or specified route pattern and response writer.
/// </summary>
[PublicAPI]
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps a health check endpoint to the specified route in the <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <param name="routeBuilder">The endpoint route builder to map the health check endpoint to.</param>
    /// <param name="pattern"></param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for the mapped health check endpoint.</returns>
    public static IEndpointConventionBuilder MapHealthChecks(
        this IEndpointRouteBuilder routeBuilder,
        [StringSyntax("Route")]string pattern = "/health"
    ) => routeBuilder.MapHealthChecks(
        pattern: pattern,
        options: new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponseNoExceptionDetails }
    );
    
}