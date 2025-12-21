using System.Reflection;

namespace Modules.Tenants.Infrastructure;

/// <summary>
/// Represents the tenants module infrastructure assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
