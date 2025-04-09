using Ardalis.Result;
using Geneirodan.MediatR.Abstractions;
using Geneirodan.MediatR.Attributes;
using MediatR;

namespace Geneirodan.SampleApi.Application.Commands;

[Authorize]
public sealed record AuthorizedCommand(bool ShouldFail) : ICommand
{
    public sealed class Handler : IRequestHandler<AuthorizedCommand, Result>
    {
        public Task<Result> Handle(AuthorizedCommand request, CancellationToken cancellationToken) => 
            Task.FromResult(request.ShouldFail ? Result.Error("SomeSortOfError") : Result.Success());
    }
}