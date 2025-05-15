namespace Geneirodan.Abstractions.Mapping;

/// <summary>
/// <inheritdoc cref="IMapper{TSource, TDestination}"/><br/>
/// Extends the <see cref="IMapper{TSource, TDestination}"/> interface to support
/// mapping between single entities as well as <see cref="IQueryable{T}"/> of entities.
/// </summary>
/// <typeparam name="TSource">
/// <inheritdoc cref="IMapper{TSource,TDestination}"/>
/// </typeparam>
/// <typeparam name="TDestination">
/// <inheritdoc cref="IMapper{TSource,TDestination}"/>
/// </typeparam>
public interface IEntityMapper<in TSource, out TDestination>
    : IMapper<TSource, TDestination>, IMapper<IQueryable<TSource>, IQueryable<TDestination>>;