using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Tenants.Domain.TenantMemberships;
using Modules.Tenants.Domain.Tenants;
using Modules.Tenants.Persistence.Constants;
using Persistence.Constants;

namespace Modules.Tenants.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="Tenant"/> entity configuration.
/// </summary>
internal sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        ConfigureDataStructure(builder);
        
        ConfigureIndexes(builder);

        ConfigureRelationships(builder);
    }

    private static void ConfigureDataStructure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable(TableNames.Tenants);
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(x => x.Value, v => new TenantId(v));
        
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(TenantName.MaxLength)
            .HasConversion(x => x.Value, v => new TenantName(v));
        
        builder
            .Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(TenantSlug.MaxLength)
            .HasConversion(x => x.Value, v => new TenantSlug(v));
        
        builder.Property<DateTime>(AuditableProperties.CreatedOnUtc).IsRequired();
        
        builder.Property<DateTime?>(AuditableProperties.ModifiedOnUtc).IsRequired(false);
    }
    
    private static void ConfigureIndexes(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasIndex(x => x.Slug).IsUnique();
    }
    
    private static void ConfigureRelationships(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasMany<TenantMembership>("_memberships")
            .WithOne()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Navigation("_memberships").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
