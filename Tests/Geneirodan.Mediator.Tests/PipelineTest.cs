using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Geneirodan.Mediator.Tests;

public abstract class PipelineTest : IClassFixture<ApiFactory>, IDisposable
{
    protected readonly ISender Sender;
    protected readonly IServiceScope Scope;

    protected PipelineTest(ApiFactory factory)
    {
        Scope = factory.Services.CreateScope();
        Sender = Scope.ServiceProvider.GetRequiredService<ISender>();
    }

    public void Dispose()
    {
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}
