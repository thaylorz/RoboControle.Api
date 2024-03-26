using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Application.Robots.Commands.ChangeRobotHeadInclination;
public sealed class ChangeRobotHeadInclinationCommandHandler(IRobotsRepository _robotsRepository) : IRequestHandler<ChangeRobotHeadInclinationCommand, ErrorOr<RobotResult>>
{
    public async Task<ErrorOr<RobotResult>> Handle(ChangeRobotHeadInclinationCommand request, CancellationToken cancellationToken)
    {
        var robot = await _robotsRepository.GetByIdAsync(request.RobotId, cancellationToken);

        if (robot is null)
        {
            return Error.NotFound(description: "Robô não encontrado.");
        }

        var result = robot.Head.ChangeInclinationState(request.Inclination);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _robotsRepository.UpdateAsync(robot, cancellationToken);
        return RobotResult.FromRobot(robot);
    }
}
