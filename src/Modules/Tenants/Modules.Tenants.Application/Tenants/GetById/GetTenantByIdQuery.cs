using Application.Messaging;

namespace Modules.Tenants.Application.Tenants.GetById;

/// <summary>
/// Represents the query for getting a tenant by its identifier.
/// </summary>
/// <param name="TenantId">The tenant identifier.</param>
public sealed record GetTenantByIdQuery(Guid TenantId) : IQuery<TenantResponse>;
