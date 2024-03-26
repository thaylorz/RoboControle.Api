using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Application.Robots.Commands.ChangeRobotWristRotation;
public record ChangeRobotWristRotationCommand(Ulid RobotId, WristRotationEnum Rotation, SideEnum Side) : IRequest<ErrorOr<RobotResult>>;