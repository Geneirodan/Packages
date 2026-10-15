using Geneirodan.Mediator.Attributes;

namespace Geneirodan.SampleApi.Application.Commands;

[Authorize(Roles = "Admin")]
public sealed record AdminCommand(bool ShouldFail) : ICommand<Result>
{
    public sealed class Handler : ICommandHandler<AdminCommand, Result>
    {
        public ValueTask<Result> Handle(AdminCommand request, CancellationToken cancellationToken) =>
            ValueTask.FromResult(request.ShouldFail ? Result.Error("SomeSortOfError") : Result.Success());
    }
}
