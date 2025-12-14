using Domain.Errors;

namespace Modules.Users.Domain.Users;

/// <summary>
/// Contains the users errors.
/// </summary>
public static class UserErrors
{
    /// <summary>
    /// Gets the email is not unique error.
    /// </summary>
    public static Error EmailIsNotUnique => Error.Conflict(
        "User.EmailIsNotUnique", 
        "The specified email is already in use."
    );
    
    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Func<UserId, Error> NotFound => userId => Error.NotFound(
        "User.NotFound",
        $"The user with the identifier '{userId.Value}' was not found."
    );

}
