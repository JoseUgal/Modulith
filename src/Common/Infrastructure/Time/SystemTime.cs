using Application.Time;

namespace Infrastructure.Time;

/// <inheritdoc cref="ISystemTime" />
public sealed class SystemTime : ISystemTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
