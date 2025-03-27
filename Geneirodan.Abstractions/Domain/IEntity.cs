namespace Geneirodan.Abstractions.Domain;

/// <summary>
/// Defines a contract for an entity that has an identifier of type <typeparamref name="TKey"/>.
/// This interface is typically used to represent domain entities with a unique identifier.
/// </summary>
/// <typeparam name="TKey">
/// The type of the identifier for the entity. It must implement <see cref="IEquatable{TKey}"/> to support 
/// equality comparisons for determining if two entity identifiers are equal.
/// </typeparam>
public interface IEntity<out TKey> 
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Unique identifier of the entity.
    /// </summary>
    public TKey Id { get; }
}
