using FluentValidation;
using JetBrains.Annotations;

namespace Geneirodan.SampleApi.Application.Commands;

public sealed record ValidatedCommand(string Email) : ICommand<Result>
{
    public sealed class Handler : ICommandHandler<ValidatedCommand, Result>
    {
        public ValueTask<Result> Handle(ValidatedCommand request, CancellationToken cancellationToken) =>
            ValueTask.FromResult(Result.Success());
    }

    [UsedImplicitly]
    public sealed class Validator : AbstractValidator<ValidatedCommand>
    {
        public Validator() => RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
