namespace Geneirodan.Abstractions.Mapping;

/// <summary>
/// Defines a contract for mapping an object of type <typeparamref name="TSource"/>
/// to an object of type <typeparamref name="TDestination"/>.
/// </summary>
/// <typeparam name="TSource">
/// The type of the source object to be mapped.
/// </typeparam>
/// <typeparam name="TDestination">
/// The type of the destination object after mapping.
/// </typeparam>
public interface IMapper<in TSource, out TDestination>
{
    /// <summary>
    /// Maps the source object to a destination object.
    /// </summary>
    /// <param name="source">
    /// The source object to be mapped.
    /// </param>
    /// <returns>
    /// The mapped destination object.
    /// </returns>
    TDestination Map(TSource source);
}