using System.ComponentModel.DataAnnotations;
using Geneirodan.Abstractions.Domain;

namespace Geneirodan.SampleApi.Persistence;

public class EfEntity : IEntity<int>
{
    public int Id { get; init; }
    [MaxLength(1024)]
    public required string Name { get; init; }
}