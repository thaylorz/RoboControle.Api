using RoboControle.Application.Robots.Common;

namespace RoboControle.Application.Robots.Commands.CreateRobot;
public record CreateRobotCommand(string Name) : IRequest<ErrorOr<RobotResult>>;
