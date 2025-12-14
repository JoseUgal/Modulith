using Domain.Results;
using Modules.Users.Domain.Users;

namespace Modules.Users.UnitTests.Domain.Users;

public sealed class UserFirstNameTests
{
    [Fact]
    public void Create_Empty_ReturnsIsRequired()
    {
        // Arrange
        string input = "";
        
        // Act
        Result<UserFirstName> result = UserFirstName.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.FirstName.IsRequired, result.Error);
    }

    [Fact]
    public void Create_TooLong_ReturnsTooLong()
    {
        // Arrange
        string input = new('a', UserFirstName.MaxLength + 1);

        // Act
        Result<UserFirstName> result = UserFirstName.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.FirstName.TooLong(UserFirstName.MaxLength), result.Error);
    }
    
    [Fact]
    public void Create_Valid_TrimsAndSuccess()
    {
        // Arrange
        string input = "   Ana   ";

        // Act
        Result<UserFirstName> result = UserFirstName.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Ana", result.Value.Value);
    }
}
