using RoboControle.Application.Robots.Common;

namespace RoboControle.Application.Robots.Queries.ListRobots;
public record ListRobotsQuery() : IRequest<ErrorOr<List<RobotResult>>>;