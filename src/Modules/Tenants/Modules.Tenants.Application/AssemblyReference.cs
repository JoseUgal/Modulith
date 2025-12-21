using System.Reflection;

namespace Modules.Tenants.Application;

/// <summary>
/// Represents the tenants module application assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
