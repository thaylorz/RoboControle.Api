using ErrorOr;

using RoboControle.Domain.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Domain.Entities.Robot.ValueObject;
public sealed class RobotHead
{
    public HeadInclinationEnum Inclination { get; private set; } = HeadInclinationEnum.Rest;
    public HeadRotationEnum Rotation { get; private set; } = HeadRotationEnum.Rest;

    public ErrorOr<Success> ChangeInclinationState(HeadInclinationEnum newState)
    {
        if (IsInvalidInclinationState(Inclination, newState))
        {
            return RobotHeadErrors.InvalidInclinationState;
        }

        Inclination = newState;

        return Result.Success;
    }

    public ErrorOr<Success> ChangeRotationState(HeadRotationEnum newState)
    {
        if (IsInvalidRotationState(Rotation, newState))
        {
            return RobotHeadErrors.InvalidRotationState;
        }

        if (Inclination == HeadInclinationEnum.Down)
        {
            return RobotHeadErrors.InvalidRotationWhileInclinedDown;
        }

        Rotation = newState;

        return Result.Success;
    }

    public void Reset()
    {
        Inclination = HeadInclinationEnum.Rest;
        Rotation = HeadRotationEnum.Rest;
    }

    public static bool IsInvalidInclinationState(HeadInclinationEnum currentState, HeadInclinationEnum newState)
    {
        return !(newState == currentState + 1 || newState == currentState - 1);
    }

    public static bool IsInvalidRotationState(HeadRotationEnum currentState, HeadRotationEnum newState)
    {
        return !(newState == currentState + 1 || newState == currentState - 1);
    }
}
