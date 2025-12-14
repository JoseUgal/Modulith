using Domain.Results;
using Modules.Users.Domain.Users;

namespace Modules.Users.UnitTests.Domain.Users;

public sealed class UserEmailTests 
{
    [Fact]
    public void Create_Empty_ReturnsIsRequired()
    {
        // Arrange
        string input = "";

        // Act
        Result<UserEmail> result = UserEmail.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Email.IsRequired.Code, result.Error.Code);
    }

    [Fact]
    public void Create_TooLong_ReturnsTooLong()
    {
        // Arrange
        const string domain = "@x.com";
        int localPartLength = UserEmail.MaxLength - domain.Length + 1;
        string input = new string('a', localPartLength) + domain;

        // Act
        Result<UserEmail> result = UserEmail.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Email.TooLong(UserEmail.MaxLength).Code, result.Error.Code);
    }

    [Fact]
    public void Create_InvalidFormat_ReturnsInvalidFormat()
    {
        // Arrange
        string input = "not-an-email";

        // Act
        Result<UserEmail> result = UserEmail.Create(input);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.Email.InvalidFormat.Code, result.Error.Code);
    }

    [Fact]
    public void Create_Valid_NormalizesAndSuccess()
    {
        // Arrange
        string input = "  Ana.Demo@Example.COM  ";

        // Act
        Result<UserEmail> result = UserEmail.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("ana.demo@example.com", result.Value.Value);
    }
}
