using Domain.Results;
using Modules.Users.Domain.Users;

namespace Modules.Users.UnitTests.Domain.Users;

public class UserPasswordTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_Empty_ReturnsIsRequired(string input)
    {
        // Act
        Result<UserPassword> result = UserPassword.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Password.IsRequired, result.Error);
    }
    
    [Fact]
    public void Create_TooShort_ReturnsTooShort()
    {
        // Arrange
        string input = "Aa1!aaa";

        // Act
        Result<UserPassword> result = UserPassword.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Password.TooShort(UserPassword.MinLength), result.Error);
    }

    [Fact]
    public void Create_TooLong_ReturnsTooLong()
    {
        // Arrange
        string input = "Aa1!" + new string('a', UserPassword.MaxLength - 4 + 1);
        
        // Act
        Result<UserPassword> result = UserPassword.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Password.TooLong(UserPassword.MaxLength), result.Error);
    }

    [Theory]
    [InlineData("aaaaaaaa")] 
    [InlineData("AAAAAAAA")] 
    [InlineData("12345678")]
    [InlineData("!!!!!!!!")]
    [InlineData("Aa1aaaaa")]
    [InlineData("Aa!aaaaa")]
    [InlineData("A1!AAAAA")]
    [InlineData("a1!aaaaa")] 
    public void Create_WeakPassword_ReturnsWeak(string input)
    {
        // Act
        Result<UserPassword> result = UserPassword.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Password.Weak, result.Error);
    }
    
    [Fact]
    public void Create_Valid_ReturnsSuccess()
    {
        // Arrange
        string input = "Aa1!aaaa";

        // Act
        Result<UserPassword> result = UserPassword.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Value.Value);
    }
}
