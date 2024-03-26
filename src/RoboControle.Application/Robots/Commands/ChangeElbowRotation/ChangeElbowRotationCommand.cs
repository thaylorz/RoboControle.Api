using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Application.Robots.Commands.ChangeElbowRotation;
public record ChangeElbowRotationCommand(Ulid RobotId, ElbowRotationEnum Rotation, SideEnum Side) : IRequest<ErrorOr<RobotResult>>;
