using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Persistence.Options;

/// <summary>
/// Configures <see cref="ConnectionStringOptions"/> by retrieving the connection string
/// named "Database" from the provided <see cref="IConfiguration"/> instance.
/// </summary>
/// <remarks>
/// Throws <see cref="InvalidOperationException"/> if the connection string is missing or empty.
/// </remarks>
internal sealed class ConnectionStringSetup(IConfiguration configuration) : IConfigureOptions<ConnectionStringOptions>
{
    private const string ConnectionStringName = "Database";

    public void Configure(ConnectionStringOptions options)
    {
        string? connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"The connection string '{ConnectionStringName}' was not configured.");
        }
        
        options.Value = connectionString;
    }
}

