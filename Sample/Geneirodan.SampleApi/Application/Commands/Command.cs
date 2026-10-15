namespace Geneirodan.SampleApi.Application.Commands;

public sealed record Command(bool ShouldFail) : ICommand<Result>
{
    public sealed class Handler : ICommandHandler<Command, Result>
    {
        public ValueTask<Result> Handle(Command request, CancellationToken cancellationToken) =>
            ValueTask.FromResult(request.ShouldFail ? throw new Exception("SomeSortOfError") : Result.Success());
    }
}
