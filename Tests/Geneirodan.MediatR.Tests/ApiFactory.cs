using Geneirodan.Abstractions.Domain;
using Geneirodan.SampleApi;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog;

namespace Geneirodan.MediatR.Tests;

[UsedImplicitly]
public sealed class ApiFactory : WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped<Mock<IUser>>(_ => new Mock<IUser>());
            services.AddScoped<IUser>(x => x.GetRequiredService<Mock<IUser>>().Object);
            services.AddSerilog((_, serilog) =>
                serilog
                    .Enrich.FromLogContext()
                    .WriteTo.TestCorrelator());
        });
        base.ConfigureWebHost(builder);
    }
}