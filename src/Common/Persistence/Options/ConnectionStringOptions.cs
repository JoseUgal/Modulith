namespace Persistence.Options;

/// <summary>
/// Holds a database connection string value. 
/// Can be implicitly converted to <see cref="string"/> for convenience.
/// </summary>
public sealed class ConnectionStringOptions
{
    public string Value { get; internal set; } = string.Empty;

    public static implicit operator string(ConnectionStringOptions connectionString) => connectionString.Value;
}

