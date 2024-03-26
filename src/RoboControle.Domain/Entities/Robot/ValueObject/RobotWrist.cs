using ErrorOr;

using RoboControle.Domain.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Domain.Entities.Robot.ValueObject;
public sealed class RobotWrist
{
    public WristRotationEnum Rotation { get; private set; } = WristRotationEnum.Rest;

    public ErrorOr<Success> ChangeRotationState(WristRotationEnum newState)
    {
        if (IsInvalidRotationState(Rotation, newState))
        {
            return RobotWristErrors.InvalidRotationState;
        }

        Rotation = newState;

        return Result.Success;
    }

    public void Reset()
    {
        Rotation = WristRotationEnum.Rest;
    }

    public static bool IsInvalidRotationState(WristRotationEnum currentState, WristRotationEnum newState)
    {
        return !(newState == currentState + 1 || newState == currentState - 1);
    }
}
