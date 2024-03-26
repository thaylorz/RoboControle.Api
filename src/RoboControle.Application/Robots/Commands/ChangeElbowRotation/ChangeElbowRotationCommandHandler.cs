using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Entities.Robot;
using RoboControle.Domain.Enums;

namespace RoboControle.Application.Robots.Commands.ChangeElbowRotation;
public sealed class ChangeElbowRotationCommandHandler(IRobotsRepository _robotsRepository) : IRequestHandler<ChangeElbowRotationCommand, ErrorOr<RobotResult>>
{
    public async Task<ErrorOr<RobotResult>> Handle(ChangeElbowRotationCommand request, CancellationToken cancellationToken)
    {
        var robot = await _robotsRepository.GetByIdAsync(request.RobotId, cancellationToken);
        if (robot is null)
        {
            return Error.NotFound(description: "Robô não encontrado.");
        }

        var arm = request.Side == SideEnum.Rigth ? robot.RightArm : robot.LeftArm;
        var result = arm.Elbow.ChangeRotationState(request.Rotation);

        if (result.IsError)
        {
            return result.Errors;
        }

        await _robotsRepository.UpdateAsync(robot, cancellationToken);
        return RobotResult.FromRobot(robot);
    }
}
