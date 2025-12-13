namespace Modules.Users.Domain.Users;

/// <summary>
/// Represents the user repository interface.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Adds the specified user to the repository.
    /// </summary>
    /// <param name="user">The user.</param>
    void Add(User user);
    
    /// <summary>
    /// Checks if the specified email is unique.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The success result if the email is unique, otherwise a failure result.</returns>
    Task<bool> IsEmailUniqueAsync(UserEmail email, CancellationToken cancellationToken = default);
}
