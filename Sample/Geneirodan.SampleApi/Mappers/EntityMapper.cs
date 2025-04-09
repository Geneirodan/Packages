using System.Linq.Expressions;
using Geneirodan.Abstractions.Mapping;
using Geneirodan.SampleApi.Domain;
using Geneirodan.SampleApi.Persistence;

namespace Geneirodan.SampleApi.Mappers;

public class EntityMapper : IEntityMapper<DomainEntity, EfEntity>
{
    private static readonly Expression<Func<DomainEntity, EfEntity>> Expression = source =>
        new EfEntity { Id = source.Id, Name = source.Name };

    public EfEntity Map(DomainEntity source) => Expression.Compile().Invoke(source);

    public IQueryable<EfEntity> Map(IQueryable<DomainEntity> source) => source.Select(Expression);
}