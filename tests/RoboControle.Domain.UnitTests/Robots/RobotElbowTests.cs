using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using RoboControle.Domain.Common;
using RoboControle.Domain.Entities.Robot.ValueObject;
using RoboControle.Domain.Enums;

using Xunit;

namespace RoboControle.Domain.UnitTests.Robots;
public sealed class RobotElbowTests
{
    [Fact]
    public void CreateRobotElbow_WithValidArguments_ShouldReturnRobotElbow()
    {
        // Arrange

        // Act
        var result = new RobotElbow();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<RobotElbow>().And.Subject.As<RobotElbow>().Rotation.Should().Be(ElbowRotationEnum.Rest);
    }

    [Theory]
    [InlineData(ElbowRotationEnum.SlightlyContracted)]
    public void ChangeRotationState_WithValidArguments_ShouldChangeRotationState(ElbowRotationEnum newState)
    {
        // Arrange
        var robotElbow = new RobotElbow();

        // Act
        var result = robotElbow.ChangeRotationState(newState);

        // Assert
        result.IsError.Should().BeFalse();
        robotElbow.Rotation.Should().Be(newState);
    }

    [Theory]
    [InlineData((ElbowRotationEnum)0)]
    [InlineData((ElbowRotationEnum)8)]
    [InlineData((ElbowRotationEnum)1.2)]
    [InlineData(ElbowRotationEnum.StronglyContract)]
    public void ChangeRotationState_WithInvalidArguments_ShouldReturnError(ElbowRotationEnum newState)
    {
        // Arrange
        var robotElbow = new RobotElbow();

        // Act
        var result = robotElbow.ChangeRotationState(newState);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(x => x.Code == RobotElbowErrors.InvalidRotationState.Code);
    }

    [Fact]
    public void Reset_WithValidArguments_ShouldResetRotation()
    {
        // Arrange
        var robotElbow = new RobotElbow();
        robotElbow.ChangeRotationState(ElbowRotationEnum.StronglyContract);

        // Act
        robotElbow.Reset();

        // Assert
        robotElbow.Rotation.Should().Be(ElbowRotationEnum.Rest);
    }
}
