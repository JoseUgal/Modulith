using Domain.Results;
using Modules.Users.Application.Users.Update;
using Modules.Users.Domain.Users;
using Modules.Users.UnitTests.Common.Mothers;
using Moq;

namespace Modules.Users.UnitTests.Application.Users.Update;

public sealed class UpdateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_UserNotFound_ReturnsNotFound_AndDoesNotSave()
    {
        // Arrange
        var sut = new UpdateUserCommandHandlerSut();
        
        var userId = Guid.NewGuid();
        
        UpdateUserCommand command = sut.ValidCommand(userId);

        sut.UserRepositoryMock.Setup(x => 
                x.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync((User?)null);

        // Act
        Result result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.NotFound(new UserId(userId)), result.Error);

        sut.VerifyGetByIdCalled(userId);
        sut.VerifyDidNotSave();
    }
    
    [Fact]
    public async Task Handle_FirstNameInvalid_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        UpdateUserCommandHandlerSut sut = new();

        var userId = Guid.NewGuid();

        UpdateUserCommand command = sut.ValidCommand(userId) with { FirstName = "" };

        sut.UserRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(UserMother.Create()); 
        
        // Act
        Result result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.FirstName.IsRequired, result.Error);
        
        sut.VerifyDidNotSave();
    }
    
    [Fact]
    public async Task Handle_LastNameInvalid_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        UpdateUserCommandHandlerSut sut = new();

        var userId = Guid.NewGuid();

        UpdateUserCommand command = sut.ValidCommand(userId) with { LastName = "" };

        sut.UserRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(UserMother.Create()); 
        
        // Act
        Result result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.LastName.IsRequired, result.Error);
        
        sut.VerifyDidNotSave();
    }
    
    [Fact]
    public async Task Handle_ValidCommand_SavesChanges_AndReturnsSuccess()
    {
        // Arrange
        UpdateUserCommandHandlerSut sut = new();
        
        var userId = Guid.NewGuid();

        UpdateUserCommand command = sut.ValidCommand(userId) with { FirstName = "Juan", LastName = "Perez" };

        User user = UserMother.Create();

        sut.UserRepositoryMock.Setup(x => 
                x.GetByIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        sut.UnitOfWorkMock.Setup(x => 
                x.SaveChangesAsync(It.IsAny<CancellationToken>())
        ).Returns(Task.CompletedTask);

        // Act
        Result result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        
        sut.VerifySavedOnce();
        
        Assert.Equal(command.FirstName, user.FirstName.Value);
        Assert.Equal(command.LastName, user.LastName.Value);
    }
}
