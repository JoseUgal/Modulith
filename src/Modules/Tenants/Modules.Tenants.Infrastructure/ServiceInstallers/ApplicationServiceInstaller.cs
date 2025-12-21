using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Tenants.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the tenants module application service installer.
/// </summary>
internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Tenants.Application.AssemblyReference.Assembly);
            }
        );
    }
}
