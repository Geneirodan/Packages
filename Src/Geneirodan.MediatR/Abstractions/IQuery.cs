using Ardalis.Result;
using JetBrains.Annotations;

namespace Geneirodan.MediatR.Abstractions;

/// <summary>
/// Represents a query that returns a generic <see cref="Result{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the value returned in the result.</typeparam>
[PublicAPI]
public interface IQuery<T> : IRequest<Result<T>>;