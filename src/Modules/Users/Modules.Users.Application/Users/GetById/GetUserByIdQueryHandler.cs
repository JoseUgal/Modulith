using Application.Messaging;
using Domain.Results;
using Modules.Users.Domain.Users;

namespace Modules.Users.Application.Users.GetById;

/// <summary>
/// Represents the <see cref="GetUserByIdQuery"/> handler.
/// </summary>
internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository
) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    /// <inheritdoc />
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var userId = new UserId(query.UserId);

        User? user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(
                UserErrors.NotFound(userId)
            );
        }

        return new UserResponse
        {
            Id = user.Id.Value,
            Email = user.Email.Value,
            FirstName = user.FirstName.Value,
            LastName = user.LastName.Value
        };
    }
}
