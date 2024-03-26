using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Application.Robots.Commands.ChangeRobotHeadInclination;
public record ChangeRobotHeadRotationCommand(Ulid RobotId, HeadRotationEnum Rotation) : IRequest<ErrorOr<RobotResult>>;