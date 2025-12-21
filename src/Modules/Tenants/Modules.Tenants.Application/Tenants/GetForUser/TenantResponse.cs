namespace Modules.Tenants.Application.Tenants.GetForUser;

/// <summary>
/// Represents the tenant response.
/// </summary>
public sealed class TenantResponse
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the slug.
    /// </summary>
    public string Slug { get; init; } = string.Empty;
}
