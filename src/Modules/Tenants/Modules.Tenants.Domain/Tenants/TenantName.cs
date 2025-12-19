using Domain.Primitives;
using Domain.Results;

namespace Modules.Tenants.Domain.Tenants;

/// <summary>
/// Represents the tenant's name as an immutable value object.
/// </summary>
public sealed class TenantName : ValueObject
{
    /// <summary>
    /// Gets the minimum allowed length.
    /// </summary>
    public const int MinLength = 2;
    
    /// <summary>
    /// Gets the maximum allowed length.
    /// </summary>
    public const int MaxLength = 100;
    
    /// <summary>
    /// Creates a validated name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    /// A <see cref="Result"/> containing either a valid <see cref="TenantName"/>
    /// or an error describing why validation failed.
    /// </returns>
    public static Result<TenantName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<TenantName>(
                TenantErrors.Name.IsRequired
            );
        }
        
        name = name.Trim();

        if (name.Length < MinLength)
        {
            return Result.Failure<TenantName>(
                TenantErrors.Name.TooShort(MinLength)
            );
        }

        if (name.Length > MaxLength)
        {
            return Result.Failure<TenantName>(
                TenantErrors.Name.TooLong(MaxLength)
            );
        }
        
        return new TenantName(name);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantName"/> class.
    /// </summary>
    /// <param name="value">The validated name.</param>
    /// <remarks>
    /// This constructor is intended for EF Core and mapping purposes only.
    /// It performs no validation. For domain-level creation and validation,
    /// use <see cref="Create(string)"/> instead.
    /// </remarks>
    public TenantName(string value) => Value = value;
    
    /// <summary>
    /// Gets the value.
    /// </summary>
    public string Value { get; }

    protected override IEnumerable<object> GetAtomicValues() => [Value];
}
