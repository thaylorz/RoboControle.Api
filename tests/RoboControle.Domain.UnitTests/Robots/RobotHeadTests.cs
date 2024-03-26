using FluentAssertions;

using RoboControle.Domain.Common;
using RoboControle.Domain.Entities.Robot.ValueObject;
using RoboControle.Domain.Enums;

using Xunit;

namespace RoboControle.Domain.UnitTests.Robots;
public sealed class RobotHeadTests
{
    [Fact]
    public void CreateRobotHead_WithValidArguments_ShouldReturnRobotHead()
    {
        // Arrange
        var initialInclination = HeadInclinationEnum.Rest;
        var initialRotation = HeadRotationEnum.Rest;

        // Act
        var result = new RobotHead();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<RobotHead>().And.Subject.As<RobotHead>().Inclination.Should().Be(initialInclination);
        result.Should().BeOfType<RobotHead>().And.Subject.As<RobotHead>().Rotation.Should().Be(initialRotation);
    }

    [Theory]
    [InlineData(HeadInclinationEnum.Up)]
    [InlineData(HeadInclinationEnum.Down)]
    public void ChangeInclinationState_WithValidArguments_ShouldChangeInclinationState(HeadInclinationEnum newInclination)
    {
        // Arrange
        var robotHead = new RobotHead();

        // Act
        var result = robotHead.ChangeInclinationState(newInclination);

        // Assert
        result.IsError.Should().BeFalse();
        robotHead.Inclination.Should().Be(newInclination);
    }

    [Theory]
    [InlineData((HeadInclinationEnum)4)]
    [InlineData((HeadInclinationEnum)0)]
    public void ChangeInclinationState_WithInvalidArguments_ShouldReturnError(HeadInclinationEnum newInclination)
    {
        // Arrange
        var robotHead = new RobotHead();

        // Act
        var result = robotHead.ChangeInclinationState(newInclination);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotHeadErrors.InvalidInclinationState.Code);
    }

    [Theory]
    [InlineData(HeadRotationEnum.NegativeRotation45)]
    [InlineData(HeadRotationEnum.Rotation45)]
    public void ChangeRotationState_WithValidArguments_ShouldChangeRotationState(HeadRotationEnum newRotation)
    {
        // Arrange
        var robotHead = new RobotHead();

        // Act
        var result = robotHead.ChangeRotationState(newRotation);

        // Assert
        result.IsError.Should().BeFalse();
        robotHead.Rotation.Should().Be(newRotation);
    }

    [Theory]
    [InlineData(HeadRotationEnum.NegativeRotation90)]
    [InlineData(HeadRotationEnum.Rotation90)]
    [InlineData((HeadRotationEnum)6)]
    [InlineData((HeadRotationEnum)0)]
    public void ChangeRotationState_WithInvalidArguments_ShouldReturnError(HeadRotationEnum newRotation)
    {
        // Arrange
        var robotHead = new RobotHead();

        // Act
        var result = robotHead.ChangeRotationState(newRotation);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotHeadErrors.InvalidRotationState.Code);
    }

    [Theory]
    [InlineData(HeadRotationEnum.NegativeRotation45)]
    [InlineData(HeadRotationEnum.Rotation45)]
    public void ChangeRotationState_WithInclinationStateDown_ShouldReturnError(HeadRotationEnum newRotation)
    {
        // Arrange
        var robotHead = new RobotHead();
        robotHead.ChangeInclinationState(HeadInclinationEnum.Down);

        // Act
        var result = robotHead.ChangeRotationState(newRotation);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotHeadErrors.InvalidRotationWhileInclinedDown.Code);
    }

    [Fact]
    public void Reset_WithValidArguments_ShouldResetRobotHead()
    {
        // Arrange
        var robotHead = new RobotHead();
        robotHead.ChangeInclinationState(HeadInclinationEnum.Up);
        robotHead.ChangeRotationState(HeadRotationEnum.Rotation45);

        // Act
        robotHead.Reset();

        // Assert
        robotHead.Inclination.Should().Be(HeadInclinationEnum.Rest);
        robotHead.Rotation.Should().Be(HeadRotationEnum.Rest);
    }
}
