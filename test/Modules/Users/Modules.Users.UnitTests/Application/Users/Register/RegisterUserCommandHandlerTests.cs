using Domain.Results;
using Modules.Users.Application.Users.Register;
using Modules.Users.Domain.Users;
using Moq;

namespace Modules.Users.UnitTests.Application.Users.Register;

public sealed class RegisterUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_FirstNameInvalid_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        RegisterUserCommandHandlerSut sut = new();
        
        RegisterUserCommand command = sut.ValidCommand() with { FirstName = "" };

        // Act
        Result<Guid> result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        
        sut.VerifyDidNotCheckUniqueness();
        
        sut.VerifyDidNotPersist();
        
        sut.VerifyDidNotHashPassword();
    }
    
    [Fact]
    public async Task Handle_LastNameInvalid_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        RegisterUserCommandHandlerSut sut = new();
        
        RegisterUserCommand command = sut.ValidCommand() with { LastName = "" };

        // Act
        Result<Guid> result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        
        sut.VerifyDidNotCheckUniqueness();
        
        sut.VerifyDidNotPersist();
        
        sut.VerifyDidNotHashPassword();
    }
    
    [Fact]
    public async Task Handle_EmailInvalid_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        RegisterUserCommandHandlerSut sut = new();
        
        RegisterUserCommand command = sut.ValidCommand() with { Email = "not-an-email" };

        // Act
        Result<Guid> result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        
        sut.VerifyDidNotCheckUniqueness();
        
        sut.VerifyDidNotPersist();
        
        sut.VerifyDidNotHashPassword();
    }
    
    [Fact]
    public async Task Handle_PasswordInvalid_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        RegisterUserCommandHandlerSut sut = new();
        
        RegisterUserCommand command = sut.ValidCommand() with { Password = "" };

        // Act
        Result<Guid> result = await sut.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        
        sut.VerifyDidNotCheckUniqueness();
        
        sut.VerifyDidNotPersist();
        
        sut.VerifyDidNotHashPassword();
    }

    [Fact]
    public async Task Handle_EmailNotUnique_ReturnsFailure_AndDoesNotPersist()
    {
        // Arrange
        RegisterUserCommandHandlerSut sut = new();
        
        RegisterUserCommand command = sut.ValidCommand();

        sut.PasswordHasherMock.Setup(x =>
            x.Hash(It.IsAny<string>())
        ).Returns("HASHED");

        sut.UserRepositoryMock.Setup(x =>
            x.IsEmailUniqueAsync(It.IsAny<UserEmail>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(false);

        // Act
        Result<Guid> result = await sut.Handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Email.IsNotUnique, result.Error);
        
        sut.VerifyDidNotPersist();
    }

    [Fact]
    public async Task Handle_ValidCommand_PersistsUser_AndReturnsId()
    {
        // Arrange
        RegisterUserCommandHandlerSut sut = new();
        
        RegisterUserCommand command = sut.ValidCommand();
     
        sut.PasswordHasherMock.Setup(x =>
            x.Hash(It.IsAny<string>())
        ).Returns("HASHED");

        sut.UserRepositoryMock.Setup(x =>
            x.IsEmailUniqueAsync(It.IsAny<UserEmail>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        sut.UnitOfWorkMock.Setup(x =>
            x.SaveChangesAsync(It.IsAny<CancellationToken>())
        ).Returns(Task.CompletedTask);
        
        // Act
        Result<Guid> result = await sut.Handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
        
        sut.VerifyHashedOnce();
        
        sut.VerifyPersistedOnce();
    }

}
