using Ardalis.Result;
using Geneirodan.MediatR.Abstractions;
using Geneirodan.MediatR.Attributes;
using MediatR;

namespace Geneirodan.SampleApi.Application.Commands;

[Authorize(Roles = "Admin")]
public sealed record AdminCommand(bool ShouldFail) : ICommand
{
    public sealed class Handler : IRequestHandler<AdminCommand, Result>
    {
        public Task<Result> Handle(AdminCommand request, CancellationToken cancellationToken) => 
            Task.FromResult(request.ShouldFail ? Result.Error("SomeSortOfError") : Result.Success());
    }
}