using ErrorOr;

using RoboControle.Domain.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Domain.Entities.Robot.ValueObject;
public sealed class RobotArm
{
    public RobotElbow Elbow { get; private set; } = new();
    public RobotWrist Wrist { get; private set; } = new();

    public ErrorOr<Success> ChangeRotationStateWrist(WristRotationEnum newState)
    {
        if (!Elbow.IsStronglyContracted)
        {
            return RobotArmErrors.InvalidWristRotationWhileElbowIsNotStronglyContracted;
        }

        return Wrist.ChangeRotationState(newState);
    }

    public ErrorOr<Success> ChangeRotationStateElbow(ElbowRotationEnum newState)
    {
        return Elbow.ChangeRotationState(newState);
    }
}
