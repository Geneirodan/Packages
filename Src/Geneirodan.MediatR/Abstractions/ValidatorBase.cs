using FluentValidation;
using JetBrains.Annotations;

namespace Geneirodan.MediatR.Abstractions;

/// <inheritdoc/>
[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public abstract class ValidatorBase<T> : AbstractValidator<T>;