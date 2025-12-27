namespace Modules.Tenants.Application.Tenants.GetMembers;

/// <summary>
/// Represents the tenant member response.
/// </summary>
public sealed class TenantMemberResponse
{
    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public Guid UserId { get; init; }
    
    /// <summary>
    /// Gets the role.
    /// </summary>
    public string Role { get; init; } = string.Empty;
    
    /// <summary>
    /// Gets the status.
    /// </summary>
    public string Status { get; init; } = string.Empty;
}
