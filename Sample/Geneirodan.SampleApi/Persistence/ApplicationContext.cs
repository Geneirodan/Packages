using Microsoft.EntityFrameworkCore;

namespace Geneirodan.SampleApi.Persistence;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) => Database.EnsureCreated();

    public DbSet<EfEntity> DbSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EfEntity>().HasData(
            new EfEntity { Id = 1, Name = "Entity1" },
            new EfEntity { Id = 2, Name = "Entity2" },
            new EfEntity { Id = 3, Name = "Entity3" },
            new EfEntity { Id = 4, Name = "Entity4" }
        );
        base.OnModelCreating(modelBuilder);
    }
}