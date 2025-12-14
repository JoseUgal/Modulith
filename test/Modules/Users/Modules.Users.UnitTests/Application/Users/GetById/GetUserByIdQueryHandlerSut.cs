using Modules.Users.Application.Users.GetById;
using Modules.Users.Domain.Users;
using Moq;

namespace Modules.Users.UnitTests.Application.Users.GetById;

internal sealed class GetUserByIdQueryHandlerSut
{
    public Mock<IUserRepository> UserRepositoryMock { get; } = new(MockBehavior.Loose);
    
    public GetUserByIdQueryHandler Handler { get; }

    public GetUserByIdQueryHandlerSut()
    {
        Handler = new GetUserByIdQueryHandler(
            UserRepositoryMock.Object
        );
    }
    
    public GetUserByIdQuery ValidQuery(Guid userId) => new(
        userId
    );
    
    public void VerifyGetByIdCalled(Guid userId)
    {
        UserRepositoryMock.Verify(
            x => x.GetByIdAsync(It.Is<UserId>(id => id.Value == userId), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
    
    public User ValidUser()
    {
        UserId id = new(Guid.NewGuid());
        UserFirstName firstName = UserFirstName.Create("Ana").Value;
        UserLastName lastName = UserLastName.Create("García").Value;
        UserEmail email = UserEmail.Create("ana@demo.com").Value;
        
        return new User(id, firstName, lastName, email, "HASH");
    }
}
