using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Application.Robots.Commands.DeleteRobot;
public sealed class DeleteRobotCommandHandler(IRobotsRepository _robotsRepository) : IRequestHandler<DeleteRobotCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteRobotCommand request, CancellationToken cancellationToken)
    {
        var robot = await _robotsRepository.GetByIdAsync(request.RobotId, cancellationToken);
        if (robot is null)
        {
            return Error.NotFound(description: "Robô não encontrado.");
        }

        await _robotsRepository.RemoveAsync(robot, cancellationToken);
        return Result.Success;
    }
}