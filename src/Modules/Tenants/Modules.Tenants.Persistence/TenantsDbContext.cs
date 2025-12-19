using Microsoft.EntityFrameworkCore;
using Modules.Tenants.Persistence.Constants;

namespace Modules.Tenants.Persistence;

/// <summary>
/// Represents the tenants module database context.
/// </summary>
public sealed class TenantsDbContext(DbContextOptions<TenantsDbContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Tenants);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
