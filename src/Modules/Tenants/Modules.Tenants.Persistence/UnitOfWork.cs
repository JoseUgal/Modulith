using Application.ServiceLifetimes;
using Modules.Tenants.Domain;

namespace Modules.Tenants.Persistence;

/// <summary>
/// Represents the tenant's module unit of work.
/// </summary>
public class UnitOfWork(TenantsDbContext dbContext) : IUnitOfWork, IScoped
{
    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
