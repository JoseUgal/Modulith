using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Tenants.Domain.Tenants;

namespace Modules.Tenants.Persistence.Repositories;

/// <summary>
/// Represents the tenant repository.
/// </summary>
/// <param name="dbContext">The database context.</param>
public sealed class TenantRepository(TenantsDbContext dbContext) : ITenantRepository, IScoped
{
    /// <inheritdoc />
    public void Add(Tenant tenant) => dbContext.Set<Tenant>().Add(tenant);

    /// <inheritdoc />
    public async Task<bool> IsSlugUniqueAsync(TenantSlug slug, CancellationToken cancellationToken = default)
    {
        return !await dbContext.Set<Tenant>().AnyAsync(user =>
                user.Slug == slug,
            cancellationToken
        );
    }
}
