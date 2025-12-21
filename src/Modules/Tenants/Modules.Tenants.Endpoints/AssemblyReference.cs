using System.Reflection;

namespace Modules.Tenants.Endpoints;

/// <summary>
/// Represents the tenants module endpoints assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
