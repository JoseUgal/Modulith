using Domain.Results;
using Modules.Users.Domain.Users;

namespace Modules.Users.UnitTests.Domain.Users;

public sealed class UserLastNameTests
{
    [Fact]
    public void Create_Empty_ReturnsIsRequired()
    {
        // Arrange
        string input = "";
        
        // Act
        Result<UserLastName> result = UserLastName.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.LastName.IsRequired, result.Error);
    }

    [Fact]
    public void Create_TooLong_ReturnsTooLong()
    {
        // Arrange
        string input = new('a', UserLastName.MaxLength + 1);

        // Act
        Result<UserLastName> result = UserLastName.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.LastName.TooLong(UserLastName.MaxLength), result.Error);
    }
    
    [Fact]
    public void Create_Valid_TrimsAndSuccess()
    {
        // Arrange
        string input = "   Garcia   ";

        // Act
        Result<UserLastName> result = UserLastName.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Garcia", result.Value.Value);
    }
}
