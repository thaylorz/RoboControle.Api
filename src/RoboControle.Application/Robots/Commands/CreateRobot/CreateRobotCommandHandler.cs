using RoboControle.Application.Robots.Common;
using RoboControle.Application.Security.CurrentUserProvider;
using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Application.Robots.Commands.CreateRobot;
public sealed class CreateRobotCommandHandler(IRobotsRepository _robotsRepository, ICurrentUserProvider _currentUserProvider) : IRequestHandler<CreateRobotCommand, ErrorOr<RobotResult>>
{
    public async Task<ErrorOr<RobotResult>> Handle(CreateRobotCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        var robotResult = RobotEntity.Create(request.Name, currentUser.Id);

        if (robotResult.IsError)
        {
            return robotResult.Errors;
        }

        var robot = robotResult.Value;
        await _robotsRepository.AddAsync(robot, cancellationToken);
        return RobotResult.FromRobot(robot);
    }
}
