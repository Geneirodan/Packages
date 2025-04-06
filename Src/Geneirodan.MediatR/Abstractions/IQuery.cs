using Ardalis.Result;

namespace Geneirodan.MediatR.Abstractions;

/// <summary>
/// Represents a query that returns a generic <see cref="Result{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the value returned in the result.</typeparam>
public interface IQuery<T> : IRequest<Result<T>>;