using Application.Messaging;

namespace Modules.Tenants.Application.Tenants.GetForUser;

/// <summary>
/// Represents the query for getting a user's tenants.
/// </summary>
/// <param name="UserId">The user identifier.</param>
public sealed record GetTenantsForUserQuery(Guid UserId) : IQuery<TenantResponse[]>;
