using System.Reflection;
using Geneirodan.MediatR.Behaviors;
using Geneirodan.MediatR.Options;
using JetBrains.Annotations;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Geneirodan.MediatR;

/// <summary>
/// Provides extension methods for registering MediatR pipeline behaviors and options in the <see cref="IServiceCollection"/>.
/// </summary>
[PublicAPI]
public static class DependencyInjection
{
    /// <summary>
    /// Registers the MediatR pipeline behaviors with default options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the pipeline behaviors to.</param>
    /// <param name="assemblies">The assemblies that contain MediatR request and handler types.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with MediatR pipeline behaviors added.</returns>
    public static IServiceCollection AddMediatRPipeline(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddMediatRPipeline(new MediatRPipelineOptions(), assemblies);

    /// <summary>
    /// Registers the MediatR pipeline behaviors with custom options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the pipeline behaviors to.</param>
    /// <param name="options">The configuration options for the MediatR pipeline behaviors.</param>
    /// <param name="assemblies">The assemblies that contain MediatR request and handler types.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with MediatR pipeline behaviors added.</returns>
    public static IServiceCollection AddMediatRPipeline(
        this IServiceCollection services,
        MediatRPipelineOptions options,
        params Assembly[] assemblies
    ) => services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblies(assemblies);

        if (options.UseLogging)
            cfg.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingPreProcessor<>));

        if (options.UseAuthorization)
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        if (options.UseValidation)
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        if (options.UseExceptions)
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
    });
}