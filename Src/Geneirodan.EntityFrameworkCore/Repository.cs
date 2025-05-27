using Geneirodan.Abstractions.Domain;
using Geneirodan.Abstractions.Mapping;
using Geneirodan.Abstractions.Repositories;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Geneirodan.EntityFrameworkCore;

/// <summary>
/// Provides an implementation of the repository pattern for managing entities in a DbContext.
/// </summary>
/// <param name="context">
/// The <see cref="DbContext"/> instance used to interact with the database.
/// </param>
/// <param name="entityMapper">
/// The <see cref="IEntityMapper{TEntity, TEfEntity}"/> used to
/// map between the domain entity and the Entity Framework entity.
/// </param>
/// <param name="reverseEntityMapper">
/// The <see cref="IEntityMapper{TEfEntity, TEntity}"/> used to
/// map from the Entity Framework entity back to the domain entity.
/// </param>
/// <typeparam name="TEntity">
/// The type of the entity that the repository manages.
/// It must implement the <see cref="IEntity{TKey}"/> interface.
/// </typeparam>
/// <typeparam name="TKey">
/// The type of the primary key of the entity. It must implement <see cref="IEquatable{TKey}"/>.
/// </typeparam>
/// <typeparam name="TEfEntity">
/// The type of the entity that is used by Entity Framework (typically a database entity).
/// </typeparam>
[PublicAPI]
public class Repository<TEntity, TKey, TEfEntity>(
    DbContext context,
    IEntityMapper<TEntity, TEfEntity> entityMapper,
    IEntityMapper<TEfEntity, TEntity> reverseEntityMapper
)
    : IRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TEfEntity : class, IEntity<TKey>
{
    /// <summary>
    /// The DbSet representing the collection of <typeparamref name="TEfEntity"/> entities in the context.
    /// </summary>
    protected DbSet<TEfEntity> Set => context.Set<TEfEntity>();

    /// <inheritdoc/>
    public virtual Task<TEntity?> FindAsync(TKey id, CancellationToken token = default) => FindAsync(Set, id, token);

    /// <inheritdoc/>
    public Task<bool> ExistsAsync(TKey id, CancellationToken token = default) => 
        Set.Where(e => e.Id.Equals(id)).AnyAsync(token);

    /// <inheritdoc cref="IRepository{TEntity,TKey}.FindAsync"/>
    protected virtual async Task<TEntity?> FindAsync(IQueryable<TEfEntity> entities, TKey id, CancellationToken token)
    {
        var queryable = entities.AsNoTracking().Where(e => e.Id.Equals(id));
        var entity = await queryable.FirstOrDefaultAsync(token).ConfigureAwait(false);
        return entity is not null ? reverseEntityMapper.Map(entity) : default;
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var efEntity = entityMapper.Map(entity);
        Set.Add(efEntity);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return reverseEntityMapper.Map(efEntity);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var efEntity = entityMapper.Map(entity);
        context.Attach(efEntity);
        Set.Update(efEntity);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return reverseEntityMapper.Map(efEntity);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var efEntity = entityMapper.Map(entity);
        context.Attach(efEntity);
        Set.Remove(efEntity);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}