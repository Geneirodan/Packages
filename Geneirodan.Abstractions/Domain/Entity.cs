namespace Geneirodan.Abstractions.Domain;

/// <summary>
/// Represents an abstract base class for entities that implement <see cref="IEntity{TKey}"/>.
/// This class provides a common implementation for entities that have a unique identifier of type <typeparamref name="TKey"/>.
/// </summary>
/// <typeparam name="TKey">
/// <inheritdoc/>
/// </typeparam>
public abstract class Entity<TKey> : IEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    /// <inheritdoc/>
    public required TKey Id { get; init; }
}
