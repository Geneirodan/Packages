using Geneirodan.Abstractions.Domain;

namespace Geneirodan.SampleApi.Persistence;

public class EfEntity : IEntity<int>
{
    public int Id { get; init; }
    public required string Name { get; set; }
}