namespace RoboControle.Application.Robots.Commands.DeleteRobot;
public record DeleteRobotCommand(Ulid RobotId) : IRequest<ErrorOr<Success>>;
