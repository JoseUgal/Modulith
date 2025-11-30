using Application.Time;
using Infrastructure.Configuration;
using Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module infrastructure service installer.
/// </summary>
internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddTransient<ISystemTime, SystemTime>();
    }
}
