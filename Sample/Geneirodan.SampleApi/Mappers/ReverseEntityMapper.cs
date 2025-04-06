using System.Linq.Expressions;
using Geneirodan.Abstractions.Mapping;
using Geneirodan.SampleApi.Domain;
using Geneirodan.SampleApi.Persistence;

namespace Geneirodan.SampleApi.Mappers;

public class ReverseEntityMapper : IEntityMapper<EfEntity, DomainEntity>
{
    private static readonly Expression<Func<EfEntity, DomainEntity>> Expression = source =>
        new DomainEntity { Id = source.Id, Name = source.Name };

    public DomainEntity Map(EfEntity source) => Expression.Compile().Invoke(source);

    public IQueryable<DomainEntity> Map(IQueryable<EfEntity> source) => source.Select(Expression);
}