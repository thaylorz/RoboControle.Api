using FluentAssertions;

using RoboControle.Domain.Entities.Robot;
using RoboControle.Domain.Entities.Robot.Errors;

using Xunit;

namespace RoboControle.Domain.UnitTests.Robots;
public sealed class RobotTests
{
    [Fact]
    public void CreateRobot_WithValidArguments_ShouldReturnRobot()
    {
        // Arrange
        var name = "Robot";
        var userId = Ulid.NewUlid();

        // Act
        var result = RobotEntity.Create(name, userId);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<RobotEntity>().And.Subject.As<RobotEntity>().Name.Should().Be(name);
        result.Value.Should().BeOfType<RobotEntity>().And.Subject.As<RobotEntity>().UserId.Should().Be(userId);
    }

    [Fact]
    public void CreateRobot_WithInvalidName_ShouldReturnError()
    {
        // Arrange
        var name = string.Empty;
        var userId = Ulid.NewUlid();

        // Act
        var result = RobotEntity.Create(name, userId);

        // Assert
        result.Errors.Should().Contain(RobotErrors.InvalidName);
    }

    [Fact]
    public void CreateRobot_WithInvalidUserId_ShouldReturnError()
    {
        // Arrange
        var name = "Robot";
        var userId = Ulid.Empty;

        // Act
        var result = RobotEntity.Create(name, userId);

        // Assert
        result.Errors.Should().Contain(x => x.Code == RobotErrors.InvalidUserId.Code);
    }
}
