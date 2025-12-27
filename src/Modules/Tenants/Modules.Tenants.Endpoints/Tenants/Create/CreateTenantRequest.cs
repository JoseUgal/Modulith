namespace Modules.Tenants.Endpoints.Tenants.Create;

/// <summary>
/// Represents the request for creating a new tenant.
/// </summary>
/// <param name="Name">The name.</param>
/// <param name="Slug">The slug.</param>
public sealed record CreateTenantRequest(
    string Name,
    string Slug
);
