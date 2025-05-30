﻿using Ardalis.Result;
using JetBrains.Annotations;

namespace Geneirodan.MediatR.Abstractions;

/// <summary>
/// Represents a command that returns a non-generic <see cref="Result"/>.
/// </summary>
[PublicAPI]
public interface ICommand : IRequest<Result>;

/// <summary>
/// Represents a command that returns a generic <see cref="Result{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the value returned in the result.</typeparam>
[PublicAPI]
public interface ICommand<T> : IRequest<Result<T>>;
