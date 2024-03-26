using RoboControle.Domain.Entities.Robot;
using RoboControle.Domain.Enums;

namespace RoboControle.Application.Robots.Common;
public record RobotResult(
    Ulid Id,
    Ulid UserId,
    string Name,
    HeadInclinationEnum HeadInclination,
    HeadRotationEnum HeadRotation,
    ElbowRotationEnum RightElbowRotation,
    ElbowRotationEnum LeftElbowRotation,
    WristRotationEnum RigthWristRotation,
    WristRotationEnum LeftWristRotation)
{
    public static RobotResult FromRobot(RobotEntity robot)
    {
        return new RobotResult(
            robot.Id,
            robot.UserId,
            robot.Name,
            robot.Head.Inclination,
            robot.Head.Rotation,
            robot.RightArm.Elbow.Rotation,
            robot.LeftArm.Elbow.Rotation,
            robot.RightArm.Wrist.Rotation,
            robot.LeftArm.Wrist.Rotation);
    }
};
