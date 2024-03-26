using ErrorOr;

using RoboControle.Domain.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Domain.Entities.Robot.ValueObject;
public sealed class RobotElbow
{
    public ElbowRotationEnum Rotation { get; private set; } = ElbowRotationEnum.Rest;
    public bool IsStronglyContracted => Rotation == ElbowRotationEnum.StronglyContract;

    public ErrorOr<Success> ChangeRotationState(ElbowRotationEnum newState)
    {
        if (IsInvalidRotationState(Rotation, newState))
        {
            return RobotElbowErrors.InvalidRotationState;
        }

        Rotation = newState;

        return Result.Success;
    }

    public void Reset()
    {
        Rotation = ElbowRotationEnum.Rest;
    }

    public static bool IsInvalidRotationState(ElbowRotationEnum currentState, ElbowRotationEnum newState)
    {
        return newState == 0 || !(newState == currentState + 1 || newState == currentState - 1);
    }
}
