namespace Geneirodan.MediatR.Attributes;

/// <summary>
/// Specifies authorization requirements for a class.
/// This attribute can be applied multiple times to the same class to enforce different authorization rules.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class AuthorizeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
    /// </summary>
    public AuthorizeAttribute() { }

    /// <summary>
    /// Gets or sets a comma-delimited list of roles that are allowed to access the resource.
    /// </summary>
    public string Roles { get; init; } = string.Empty;
}