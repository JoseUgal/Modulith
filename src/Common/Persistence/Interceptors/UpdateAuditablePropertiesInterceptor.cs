using Application.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistence.Constants;

namespace Persistence.Interceptors;

/// <summary>
/// Represents the interceptor for updating auditable shadow property values.
/// </summary>
public sealed class UpdateAuditablePropertiesInterceptor(ISystemTime systemTime) : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        DateTime utcNow = systemTime.UtcNow;

        foreach (EntityEntry entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                TryUpdateProperty(entry, AuditableProperties.CreatedOnUtc, utcNow);
            }

            if (entry.State == EntityState.Modified)
            {
                TryUpdateProperty(entry, AuditableProperties.ModifiedOnUtc, utcNow);
            }
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void TryUpdateProperty(EntityEntry entry, string propertyName, object value)
    {
        IProperty? property = entry.Metadata.FindProperty(propertyName);

        if (property is null)
        {
            return;
        }
        
        entry.Property(propertyName).CurrentValue = value;
    }
}
