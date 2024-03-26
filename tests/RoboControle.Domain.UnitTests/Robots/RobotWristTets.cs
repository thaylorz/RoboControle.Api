using FluentAssertions;

using RoboControle.Domain.Common;
using RoboControle.Domain.Entities.Robot.ValueObject;
using RoboControle.Domain.Enums;

using Xunit;

namespace RoboControle.Domain.UnitTests.Robots;
public sealed class RobotWristTets
{
    [Fact]
    public void CreateRobotWrist_WithValidArguments_ShouldReturnRobotWrist()
    {
        // Arrange

        // Act
        var result = new RobotWrist();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<RobotWrist>().And.Subject.As<RobotWrist>().Rotation.Should().Be(WristRotationEnum.Rest);
    }

    [Theory]
    [InlineData(WristRotationEnum.NegativeRotation45)]
    [InlineData(WristRotationEnum.Rotarion45)]
    public void ChangeRotationState_WithValidArguments_ShouldChangeRotationState(WristRotationEnum newState)
    {
        // Arrange
        var robotWrist = new RobotWrist();

        // Act
        var result = robotWrist.ChangeRotationState(newState);

        // Assert
        result.IsError.Should().BeFalse();
        robotWrist.Rotation.Should().Be(newState);
    }

    [Theory]
    [InlineData((WristRotationEnum)0)]
    [InlineData((WristRotationEnum)8)]
    [InlineData((WristRotationEnum)1.2)]
    [InlineData(WristRotationEnum.Rotarion180)]
    public void ChangeRotationState_WithInvalidArguments_ShouldReturnError(WristRotationEnum newState)
    {
        // Arrange
        var robotWrist = new RobotWrist();

        // Act
        var result = robotWrist.ChangeRotationState(newState);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotWristErrors.InvalidRotationState.Code);
    }

    [Fact]
    public void Reset_WithValidArguments_ShouldResetRotation()
    {
        // Arrange
        var robotWrist = new RobotWrist();
        robotWrist.ChangeRotationState(WristRotationEnum.NegativeRotation45);

        // Act
        robotWrist.Reset();

        // Assert
        robotWrist.Rotation.Should().Be(WristRotationEnum.Rest);
    }
}
