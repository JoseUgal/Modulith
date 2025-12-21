using Application.Data;
using Application.Messaging;
using Domain.Results;
using Modules.Tenants.Domain.TenantMemberships;

namespace Modules.Tenants.Application.Tenants.GetForUser;

/// <summary>
/// Represents the <see cref="GetTenantsForUserQuery"/> handler.
/// </summary>
internal sealed class GetTenantsForUserQueryHandler(ISqlQueryExecutor sqlQueryExecutor) : IQueryHandler<GetTenantsForUserQuery, TenantResponse[]>
{
    /// <inheritdoc />
    public async Task<Result<TenantResponse[]>> Handle(GetTenantsForUserQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<TenantResponse> tenants = await GetTenantsForUserAsync(query.UserId);

        return tenants.ToArray();
    }

    private async Task<IEnumerable<TenantResponse>> GetTenantsForUserAsync(Guid userId)
    {
        return await sqlQueryExecutor.QueryAsync<TenantResponse>(
            """
                SELECT t.id, t.name, t.slug
                FROM tenants.tenants t
                    INNER JOIN tenants.tenant_memberships tm ON t.id = tm.tenant_id
                WHERE tm.user_id = @userId AND tm.status = @status
            """,
            new { userId, status = nameof(TenantMembershipStatus.Active) }
        );
    }
}
