using JetBrains.Annotations;

namespace Geneirodan.Abstractions.Repositories;

/// <summary>
/// Represents a paginated result set for a collection of items.
/// </summary>
/// <typeparam name="T">
/// The type of items contained in the paginated result.
/// </typeparam>
[PublicAPI]
public record PageModel<T>
{
    /// <summary>
    /// The collection of items for the current page.
    /// </summary>
    public required IEnumerable<T> Items { get; init; }

    /// <summary>
    /// The current page number.
    /// </summary>
    public required int Page { get; init; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public required int PageSize { get; init; }

    /// <summary>
    /// The total number of items across all pages.
    /// </summary>
    public required long TotalCount { get; init; }

    /// <summary>
    /// Gets a value indicating whether there is a next page of items.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Gets a value indicating whether there is a previous page of items.
    /// </summary>
    public bool HasPrevPage => Page > 1;

    /// <summary>
    /// Gets the total number of pages based on the total count of items and items per page.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}