using Modules.Users.Application.Abstractions.Security;
using Modules.Users.Application.Users.Register;
using Modules.Users.Domain;
using Modules.Users.Domain.Users;
using Moq;

namespace Modules.Users.UnitTests.Application.Users.Register;

internal sealed class RegisterUserCommandHandlerSut
{
    public Mock<IPasswordHasher>  PasswordHasherMock { get; } = new(MockBehavior.Strict);
    
    public Mock<IUserRepository> UserRepositoryMock { get; } = new(MockBehavior.Loose);
    
    public Mock<IUnitOfWork> UnitOfWorkMock { get; } = new(MockBehavior.Strict);

    public RegisterUserCommandHandler Handler { get; }

    public RegisterUserCommandHandlerSut()
    {
        Handler = new RegisterUserCommandHandler(
            PasswordHasherMock.Object,
            UserRepositoryMock.Object,
            UnitOfWorkMock.Object
        );
    }

    public RegisterUserCommand ValidCommand() => new(
        "ana@demo.es",
        "Ana",
        "García",
        "P@ssw0rd!"
    );
    
    public void VerifyDidNotPersist()
    {
        UserRepositoryMock.Verify(
            x => x.Add(It.IsAny<User>()),
            Times.Never
        );

        UnitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
    
    public void VerifyDidNotCheckUniqueness()
    {
        UserRepositoryMock.Verify(
            x => x.IsEmailUniqueAsync(It.IsAny<UserEmail>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
    
    public void VerifyDidNotHashPassword()
    {
        PasswordHasherMock.Verify(
            x => x.Hash(It.IsAny<string>()),
            Times.Never
        );
    }
    
    public void VerifyPersistedOnce()
    {
        UserRepositoryMock.Verify(
            x => x.Add(It.IsAny<User>()),
            Times.Once
        );

        UnitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
    
    public void VerifyHashedOnce()
    {
        PasswordHasherMock.Verify(
            x => x.Hash(It.IsAny<string>()),
            Times.Once
        );
    }
}
