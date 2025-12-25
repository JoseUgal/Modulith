using Application.Data;
using Application.Messaging;
using Domain.Results;
using Modules.Tenants.Domain.Tenants;

namespace Modules.Tenants.Application.Tenants.GetById;

/// <summary>
/// Represents the <see cref="GetTenantByIdQuery"/> handler.
/// </summary>
internal sealed class GetTenantByIdQueryHandler(ISqlQueryExecutor sqlQueryExecutor) : IQueryHandler<GetTenantByIdQuery, TenantResponse>
{
    /// <inheritdoc />
    public async Task<Result<TenantResponse>> Handle(GetTenantByIdQuery query, CancellationToken cancellationToken)
    {
        var tenantId = new TenantId(query.TenantId);
        
        TenantResponse? tenant = await sqlQueryExecutor.FirstOrDefaultAsync<TenantResponse>(
            """
                SELECT id, name, slug
                FROM tenants.tenants
                WHERE tenants.id = @tenantId
            """,
            new { tenantId = tenantId.Value }
        );

        if (tenant == null)
        {
            return Result.Failure<TenantResponse>(
                TenantErrors.NotFound(tenantId)
            );
        }

        return tenant;
    }
}
