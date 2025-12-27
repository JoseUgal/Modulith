using Application.Messaging;

namespace Modules.Tenants.Application.Tenants.GetMembers;

/// <summary>
/// Represents the query for getting a tenant members.
/// </summary>
/// <param name="TenantId">The tenant identifier.</param>
public sealed record GetTenantMembersQuery(Guid TenantId) : IQuery<TenantMemberResponse[]>;
