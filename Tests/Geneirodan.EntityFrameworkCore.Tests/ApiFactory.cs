using Geneirodan.Abstractions.Domain;
using Geneirodan.Abstractions.Repositories;
using Geneirodan.SampleApi;
using Geneirodan.SampleApi.Persistence;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Testcontainers.PostgreSql;

namespace Geneirodan.EntityFrameworkCore.Tests;

[UsedImplicitly]
public sealed class ApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:latest").Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var mock = new Mock<IUser>();
            services.AddScoped<IUser>(_ => mock.Object);
            services.AddDbContext<DbContext, ApplicationContext>(x => x.UseNpgsql(_postgres.GetConnectionString()));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        });
        base.ConfigureWebHost(builder);
    }

    public async ValueTask InitializeAsync() => await _postgres.StartAsync(TestContext.Current.CancellationToken);

    public override async ValueTask DisposeAsync()
    {
        await _postgres.DisposeAsync();
        await base.DisposeAsync();
    }
}
