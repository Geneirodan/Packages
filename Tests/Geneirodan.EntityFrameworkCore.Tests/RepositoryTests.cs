using Geneirodan.Abstractions.Repositories;
using Geneirodan.SampleApi;
using Geneirodan.SampleApi.Domain;
using Geneirodan.SampleApi.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Geneirodan.EntityFrameworkCore.Tests;

public sealed class RepositoryTests : IClassFixture<WebApplicationFactory<IApiMarker>>, IDisposable
{
    private readonly IRepository<DomainEntity, int> _repository;
    private readonly DbContext _context;
    private readonly IServiceScope _scope;

    public RepositoryTests(WebApplicationFactory<IApiMarker> factory)
    {
        _scope = factory.Services.CreateScope();
        _repository = _scope.ServiceProvider.GetRequiredService<IRepository<DomainEntity, int>>();
        _context = _scope.ServiceProvider.GetRequiredService<DbContext>();
    }

    [Fact]
    public async Task FindAsync_ReturnsMappedEntity_WhenEntityExists()
    {

        var result = await _repository.FindAsync(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Name.ShouldBe("Entity1");
    }
    
    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenEntityExists()
    {
        var result = await _repository.ExistsAsync(1);
        result.ShouldBeTrue();
    }
    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenEntityDoesNotExists()
    {
        var result = await _repository.ExistsAsync(128);
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task AddAsync_AddsEntityAndReturnsMappedEntity()
    {
        var domainEntity = new DomainEntity { Id = 5, Name = "Entity5" };

        var result = await _repository.AddAsync(domainEntity);

        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(domainEntity);
        var entity = await _repository.FindAsync(domainEntity.Id);
        entity.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesEntityAndReturnsMappedEntity()
    {
        var domainEntity = new DomainEntity { Id = 3, Name = "NewName" };

        var result = await _repository.UpdateAsync(domainEntity);

        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(domainEntity);
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntity()
    {
        var domainEntity = new DomainEntity { Id = 4, Name = "Entity4" };
        
        await _repository.DeleteAsync(domainEntity);
        var result = await _context.Set<EfEntity>().FirstOrDefaultAsync(x => x.Id == domainEntity.Id);
        result.ShouldBeNull();
    }

    public void Dispose()
    {
        _context.Dispose();
        _scope.Dispose();
    }
}
