using System.Reflection;

namespace Modules.Tenants.Persistence;

/// <summary>
/// Represents the tenants module persistence assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
