using Geneirodan.MediatR.Behaviors;
using Geneirodan.SampleApi.Application.Commands;
using JetBrains.Annotations;
using Serilog.Events;
using Serilog.Sinks.TestCorrelator;
using Shouldly;

namespace Geneirodan.MediatR.Tests.Behaviors;

[TestSubject(typeof(UnhandledExceptionBehavior<,>))]
public class UnhandledExceptionBehaviorTests(ApiFactory factory) : PipelineTest(factory)
{
    [Fact]
    public async Task UnhandledExceptionBehavior_ShouldLogException()
    {
        var command = new Command(true);
        await Should.ThrowAsync<Exception>(async () => await Sender.Send(command));
        var events = TestCorrelator.GetLogEventsFromCurrentContext();
        var entry = events.FirstOrDefault(x =>
            x.MessageTemplate.Text == "Request: Unhandled Exception for Request {RequestName}");
        entry.ShouldNotBeNull();
        entry.Properties.ShouldContainKeyAndValue("RequestName", new ScalarValue(nameof(Command)));
        entry.Exception.ShouldNotBeNull();
        entry.Exception.Message.ShouldBeEquivalentTo("SomeSortOfError");
    }
}