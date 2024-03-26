using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Application.Robots.Commands.ChangeRobotHeadInclination;
public sealed class ChangeRobotHeadRotationCommandHandler(IRobotsRepository _robotsRepository) : IRequestHandler<ChangeRobotHeadRotationCommand, ErrorOr<RobotResult>>
{
    public async Task<ErrorOr<RobotResult>> Handle(ChangeRobotHeadRotationCommand request, CancellationToken cancellationToken)
    {
        var robot = await _robotsRepository.GetByIdAsync(request.RobotId, cancellationToken);

        if (robot is null)
        {
            return Error.NotFound(description: "Robô não encontrado.");
        }

        var result = robot.Head.ChangeRotationState(request.Rotation);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _robotsRepository.UpdateAsync(robot, cancellationToken);
        return RobotResult.FromRobot(robot);
    }
}
