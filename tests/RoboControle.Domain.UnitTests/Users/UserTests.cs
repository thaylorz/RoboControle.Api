using FluentAssertions;

using RoboControle.Domain.Entities;

using Xunit;

namespace RoboControle.Domain.UnitTests.Users;
public sealed class UserTests
{
    [Fact]
    public void CreateUser_WithValidArguments_ShouldReturnUser()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var passwordSalt = "salt";
        var passwordHash = "hash";

        // Act
        var result = UserEntity.Create(firstName, lastName, email, passwordSalt, passwordHash);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<UserEntity>().And.Subject.As<UserEntity>().FirstName.Should().Be(firstName);
    }

    [Fact]
    public void CreateUser_WithInvalidFirstName_ShouldReturnError()
    {
        // Arrange
        var firstName = string.Empty;
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var passwordSalt = "salt";
        var passwordHash = "hash";

        // Act
        var result = UserEntity.Create(firstName, lastName, email, passwordSalt, passwordHash);

        // Assert
        result.Errors.Should().Contain(UserErrors.InvalidFirstName);
    }

    [Fact]
    public void CreateUser_WithInvalidLastName_ShouldReturnError()
    {
        // Arrange
        var firstName = string.Empty;
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var passwordSalt = "salt";
        var passwordHash = "hash";

        // Act
        var result = UserEntity.Create(firstName, lastName, email, passwordSalt, passwordHash);

        // Assert
        result.Errors.Should().Contain(x => x.Code == UserErrors.InvalidLastName.Code);
    }

    [Fact]
    public void CreateUser_WithInvalidEmail_ShouldReturnError()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe";
        var passwordSalt = "salt";
        var passwordHash = "hash";

        // Act
        var result = UserEntity.Create(firstName, lastName, email, passwordSalt, passwordHash);

        // Assert
        result.Errors.Should().Contain(x => x.Code == UserErrors.InvalidEmail.Code);
    }
}