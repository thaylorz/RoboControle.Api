using ErrorOr;

using RoboControle.Domain.Common;
using RoboControle.Domain.Entities.Robot.Errors;
using RoboControle.Domain.Entities.Robot.ValueObject;

namespace RoboControle.Domain.Entities.Robot;

public sealed class RobotEntity : Entity
{
    private RobotEntity() { }

    public Ulid UserId { get; private init; }
    public string Name { get; private set; } = null!;

    public RobotHead Head { get; private set; } = new();
    public RobotArm RightArm { get; private set; } = new();
    public RobotArm LeftArm { get; private set; } = new();

    public static ErrorOr<RobotEntity> Create(string name, Ulid userId)
    {
        if (IsInvalidValidName(name))
        {
            return RobotErrors.InvalidName;
        }

        if (IsInvalidUserId(userId))
        {
            return RobotErrors.InvalidUserId;
        }

        var robot = new RobotEntity
        {
            UserId = userId,
            Name = name,
        };

        return robot;
    }

    public ErrorOr<Success> Update(string name)
    {
        if (IsInvalidValidName(name))
        {
            return RobotErrors.InvalidName;
        }

        Name = name;

        return Result.Success;
    }

    private static bool IsInvalidValidName(string name)
    {
        return string.IsNullOrWhiteSpace(name);
    }

    private static bool IsInvalidUserId(Ulid userId)
    {
        return userId == Ulid.Empty;
    }
}