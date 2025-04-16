using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.TestCorrelator;

namespace Geneirodan.MediatR.Tests;

public abstract class PipelineTest : IClassFixture<ApiFactory>, IDisposable
{
    protected readonly ISender Sender;
    protected readonly IServiceScope Scope;
    private readonly ITestCorrelatorContext _context;

    protected PipelineTest(ApiFactory factory)
    {
        Scope = factory.Services.CreateScope();
        Sender = Scope.ServiceProvider.GetRequiredService<ISender>();
        _context = TestCorrelator.CreateContext();
    }

    public void Dispose()
    {
        Scope.Dispose();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}