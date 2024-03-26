using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Enums;

namespace RoboControle.Application.Robots.Commands.ChangeRobotHeadInclination;
public record ChangeRobotHeadInclinationCommand(Ulid RobotId, HeadInclinationEnum Inclination) : IRequest<ErrorOr<RobotResult>>;