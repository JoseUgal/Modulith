using Domain.Errors;
using Domain.Primitives;
using Domain.Results;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the user's last name as an immutable value object.
/// </summary>
/// <remarks>
/// Leading and trailing spaces are trimmed to maintain consistency.
/// </remarks>
public sealed class UserLastName : ValueObject
{
    /// <summary>
    /// Gets the maximum allowed length.
    /// </summary>
    public const int MaxLength = 150;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="UserLastName"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <remarks>
    /// This constructor is intended for EF Core and mapping purposes only.
    /// It performs no validation. For domain-level creation and validation,
    /// use <see cref="Create(string)"/> instead.
    /// </remarks>
    public UserLastName(string value) => Value = value;
    
    /// <summary>
    /// Gets the value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues() => [Value];

    /// <summary>
    /// Creates a validated last name.
    /// </summary>
    /// <param name="lastName">The primitive string value.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing either a valid <see cref="UserLastName"/>
    /// or an error describing why validation failed.
    /// </returns>
    public static Result<UserLastName> Create(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<UserLastName>(
                UserErrors.LastName.IsRequired
            );
        }
        
        lastName = lastName.Trim();

        if (lastName.Length > MaxLength)
        {
            return Result.Failure<UserLastName>(
                UserErrors.LastName.TooLong(MaxLength)
            );
        }

        return new UserLastName(lastName);
    }
}
