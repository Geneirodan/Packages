using Geneirodan.Abstractions.Mapping;
using JetBrains.Annotations;

namespace Geneirodan.EntityFrameworkCore;

/// <summary>
/// Provides extension methods for mapping entities or collections of entities.
/// </summary>
[PublicAPI]
public static class MappingExtensions
{
    /// <summary>
    /// Projects a queryable collection of <typeparamref name="TSource"/>
    /// to a queryable collection of <typeparamref name="TDestination"/>.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the source entities in the collection.
    /// </typeparam>
    /// <typeparam name="TDestination">
    /// The type of the destination entities after mapping.
    /// </typeparam>
    /// <param name="queryable">
    /// The queryable collection of <typeparamref name="TSource"/> entities to be mapped.
    /// </param>
    /// <param name="mapper">
    /// An instance of <see cref="IMapper{TSource, TDestination}"/>
    /// used to map the source collection to the destination collection.
    /// </param>
    /// <returns>
    /// A <see cref="IQueryable{TDestination}"/> representing the mapped collection of <typeparamref name="TDestination"/> entities.
    /// </returns>
    public static IQueryable<TDestination> ProjectTo<TSource, TDestination>(
        this IQueryable<TSource> queryable,
        IMapper<IQueryable<TSource>, IQueryable<TDestination>> mapper
    ) => mapper.Map(queryable);
}