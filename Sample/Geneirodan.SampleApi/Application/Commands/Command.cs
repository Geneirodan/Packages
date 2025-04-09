using Ardalis.Result;
using FluentValidation;
using Geneirodan.MediatR.Abstractions;
using JetBrains.Annotations;
using MediatR;

namespace Geneirodan.SampleApi.Application.Commands;

public sealed record Command(bool ShouldFail) : ICommand
{
    public sealed class Handler : IRequestHandler<Command, Result>
    {
        public Task<Result> Handle(Command request, CancellationToken cancellationToken) =>
            Task.FromResult(request.ShouldFail ? throw new Exception("SomeSortOfError") : Result.Success());
    }
}

public sealed record ValidatedCommand(string Email) : ICommand
{
    public sealed class Handler : IRequestHandler<ValidatedCommand, Result>
    {
        public Task<Result> Handle(ValidatedCommand request, CancellationToken cancellationToken) =>
            Task.FromResult(Result.Success());
    }

    [UsedImplicitly]
    public sealed class Validator : AbstractValidator<ValidatedCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}