using FluentAssertions;

using RoboControle.Domain.Common;
using RoboControle.Domain.Entities.Robot.ValueObject;
using RoboControle.Domain.Enums;

using Xunit;

namespace RoboControle.Domain.UnitTests.Robots;
public sealed class RobotArmTests
{
    [Fact]
    public void CreateRobotArm_WithValidArguments_ShouldReturnRobotArm()
    {
        // Arrange

        // Act
        var result = new RobotArm();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<RobotArm>().And.Subject.As<RobotArm>().Elbow.Should().NotBeNull();
        result.Should().BeOfType<RobotArm>().And.Subject.As<RobotArm>().Wrist.Should().NotBeNull();
    }

    [Theory]
    [InlineData(WristRotationEnum.NegativeRotation45)]
    [InlineData(WristRotationEnum.Rotarion45)]
    public void ChangeRotationStateWrist_WithValidArguments_ShouldChangeRotationStateWrist(WristRotationEnum newState)
    {
        // Arrange
        var robotArm = new RobotArm();
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.StronglyContract);
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.SlightlyContracted);
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.Contracted);
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.StronglyContract);

        // Act
        var result = robotArm.ChangeRotationStateWrist(newState);

        // Assert
        result.IsError.Should().BeFalse();
        robotArm.Wrist.Rotation.Should().Be(newState);
    }

    [Theory]
    [InlineData((WristRotationEnum)0)]
    [InlineData((WristRotationEnum)8)]
    [InlineData(WristRotationEnum.Rotarion180)]
    public void ChangeRotationStateWrist_WithInvalidArguments_ShouldReturnError(WristRotationEnum newState)
    {
        // Arrange
        var robotArm = new RobotArm();
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.StronglyContract);
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.SlightlyContracted);
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.Contracted);
        robotArm.Elbow.ChangeRotationState(ElbowRotationEnum.StronglyContract);

        // Act
        var result = robotArm.ChangeRotationStateWrist(newState);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotWristErrors.InvalidRotationState.Code);
    }

    [Theory]
    [InlineData(ElbowRotationEnum.Contracted)]
    [InlineData(ElbowRotationEnum.Rest)]
    [InlineData(ElbowRotationEnum.SlightlyContracted)]
    public void ChangeRotationStateWrist_WithInvalidElbowState_ShouldReturnError(ElbowRotationEnum newState)
    {
        // Arrange
        var robotArm = new RobotArm();
        robotArm.Elbow.ChangeRotationState(newState);

        // Act
        var result = robotArm.ChangeRotationStateWrist(WristRotationEnum.Rotarion45);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotArmErrors.InvalidWristRotationWhileElbowIsNotStronglyContracted.Code);
    }

    [Theory]
    [InlineData(ElbowRotationEnum.SlightlyContracted)]
    public void ChangeRotationStateElbow_WithValidArguments_ShouldChangeRotationStateElbow(ElbowRotationEnum newState)
    {
        // Arrange
        var robotArm = new RobotArm();

        // Act
        var result = robotArm.ChangeRotationStateElbow(newState);

        // Assert
        result.IsError.Should().BeFalse();
        robotArm.Elbow.Rotation.Should().Be(newState);
    }

    [Theory]
    [InlineData((ElbowRotationEnum)0)]
    [InlineData((ElbowRotationEnum)8)]
    [InlineData(ElbowRotationEnum.StronglyContract)]
    [InlineData(ElbowRotationEnum.Contracted)]
    public void ChangeRotationStateElbow_WithInvalidArguments_ShouldReturnError(ElbowRotationEnum newState)
    {
        // Arrange
        var robotArm = new RobotArm();

        // Act
        var result = robotArm.ChangeRotationStateElbow(newState);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotElbowErrors.InvalidRotationState.Code);
    }
}
