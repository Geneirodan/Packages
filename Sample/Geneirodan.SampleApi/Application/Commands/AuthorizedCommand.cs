using Geneirodan.Mediator.Attributes;

namespace Geneirodan.SampleApi.Application.Commands;

[Authorize]
public sealed record AuthorizedCommand(bool ShouldFail) : ICommand<Result>
{
    public sealed class Handler : ICommandHandler<AuthorizedCommand, Result>
    {
        public ValueTask<Result> Handle(AuthorizedCommand request, CancellationToken cancellationToken) =>
            ValueTask.FromResult(request.ShouldFail ? Result.Error("SomeSortOfError") : Result.Success());
    }
}
