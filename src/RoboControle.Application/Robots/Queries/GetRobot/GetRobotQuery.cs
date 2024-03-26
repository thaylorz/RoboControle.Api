using RoboControle.Application.Robots.Common;

namespace RoboControle.Application.Robots.Queries.GetRobot;
public record GetRobotQuery(Ulid RobotId) : IRequest<ErrorOr<RobotResult>>;
