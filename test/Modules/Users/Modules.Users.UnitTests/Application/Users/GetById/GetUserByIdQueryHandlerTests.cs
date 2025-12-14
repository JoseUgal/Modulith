using Domain.Results;
using Modules.Users.Application.Users.GetById;
using Modules.Users.Domain.Users;
using Moq;

namespace Modules.Users.UnitTests.Application.Users.GetById;

public sealed class GetUserByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_UserNotFound_ReturnsNotFound()
    {
        // Arrange
        GetUserByIdQueryHandlerSut sut = new();
        
        var userId = Guid.NewGuid();
        
        GetUserByIdQuery query = sut.ValidQuery(userId);

        sut.UserRepositoryMock.Setup(x => 
                x.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync((User?)null);

        // Act
        Result<UserResponse> result = await sut.Handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.NotFound(new UserId(userId)), result.Error);

        sut.VerifyGetByIdCalled(userId);
    }
    
    [Fact]
    public async Task Handle_UserExists_ReturnsMappedUserResponse()
    {
        // Arrange
        var sut = new GetUserByIdQueryHandlerSut();

        User user = sut.ValidUser();
        
        GetUserByIdQuery query = sut.ValidQuery(user.Id.Value);

        sut.UserRepositoryMock.Setup(x => 
            x.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        // Act
        Result<UserResponse> result = await sut.Handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(user.Id.Value, result.Value.Id);
        Assert.Equal(user.Email.Value, result.Value.Email);
        Assert.Equal(user.FirstName.Value, result.Value.FirstName);
        Assert.Equal(user.LastName.Value, result.Value.LastName);

        sut.VerifyGetByIdCalled(user.Id.Value);
    }
}
