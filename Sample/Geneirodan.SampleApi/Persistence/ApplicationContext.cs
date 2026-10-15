using Geneirodan.SampleApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Geneirodan.SampleApi.Persistence;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) => Database.EnsureCreated();

    public DbSet<DomainEntity> DbSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainEntity>().HasKey(x => x.Id);

        modelBuilder.Entity<DomainEntity>().Property(x => x.Name).HasMaxLength(50);

        base.OnModelCreating(modelBuilder);
    }
}
